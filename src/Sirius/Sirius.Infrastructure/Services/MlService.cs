using System.ClientModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Chat;
using OpenAI.Files;
using Sirius.Application.Forms;
using Sirius.Application.Interfaces;

namespace Sirius.Infrastructure.Services;

public class MlService(IConfiguration configuration, IFormsService formsService) : IMlService
{
    public async Task<IEnumerable<FormFieldDto>> GetFormFieldsFromSourceCode(string sourceCode)
    {
        var openaiApiKey = configuration["OpenAI:ApiKey"];
        ChatClient client = new(model: "gpt-4o-mini", openaiApiKey);
        
        var inputChatMessage = $"Given the following website source-code, please provide the included form fields in form of a json array. For every form-field, please return a json-object that looks like the following: {{'field-id', 'field-label', 'input-type', 'required-field', 'field-options'}}. The field-id is the id of the input field. The field-label is the label for that field that is presented to the user. input-type is the type of the html-input. required-field tells if the field can be empty or not. Please return true if so and false if not. If it is unclear return true. field-options is a list of options from which the user can choose. This applies for example to radio-buttons or drop-down fields. Please return a semicolon-separated string with all the options for the corresponding form-field. Return only the raw json string. Also no additional explanation or any other text. Also no additional explanation or any other text. Format the answer so that it can directly be parsed in c# which means no linebreaks or indentation. Source-code: {sourceCode}";

        //Todo set the completion parameters -> temperature
        ChatCompletion chatCompletion = await client.CompleteChatAsync([new UserChatMessage(inputChatMessage)]);
        
        
        var formJsonArray = JArray.Parse(chatCompletion.ToString());
        var result = new List<FormFieldDto>();
        foreach (var formField in formJsonArray)
        {
            var fieldId = formField["field-id"].ToString();
            var fieldLabel = formField["field-label"].ToString();
            var inputType = formField["input-type"].ToString();
            var requiredField = formField["required-field"].ToString();
            var fieldOptions = formField["field-options"].ToString();
            result.Add(new FormFieldDto(fieldId, fieldLabel, inputType, requiredField, fieldOptions));
        }
        return result;
    }
    
    [Experimental("OPENAI001")]
    public async Task<IEnumerable<FormFieldDto>> GetFormFieldsFromPdf(byte[] pdf)
    {
        var openaiApiKey = configuration["OpenAI:ApiKey"];
        OpenAIClient client = new(openaiApiKey);
        FileClient fileClient = client.GetFileClient();
        AssistantClient assistantClient = client.GetAssistantClient();
        
        var document = BinaryData.FromBytes(pdf).ToStream();
        
        
        OpenAIFileInfo uploadedFile = await fileClient.UploadFileAsync(document, "file.pdf", FileUploadPurpose.Assistants);
        AssistantCreationOptions assistantOptions = new()
        {
            Name = "Sirius File Assistant",
            // Instructions =
            //     "Please give me all form fields and their corresponding label in a json object",
            Tools =
            {
                new FileSearchToolDefinition(),
                new CodeInterpreterToolDefinition(),
            },
            ToolResources = new()
            {
                FileSearch = new()
                {
                    NewVectorStores =
                    {
                        new VectorStoreCreationHelper([uploadedFile]),
                    }
                }
            },
        };

        Assistant assistant = await assistantClient.CreateAssistantAsync("gpt-4o-mini", assistantOptions);
        
        ThreadCreationOptions threadOptions = new()
        {
            InitialMessages = { "Please provide the included form fields in form of a json array. For every form-field, please return a json-object that looks like the following: {'field-id', 'field-label', 'input-type', 'required-field', 'field-options'}. The field-id is the id of the input field. The field-label is the label for that field that is presented to the user. input-type is the type of the input. required-field tells if the field can be empty or not. Please return true if so and false if not. If it is unclear return true. field-options is a list of options from which the user can choose. This applies for example to radio-buttons or drop-down fields. Please return a semicolon-separated string with all the options for the corresponding form-field. Return a valid json object that can be parsed in the c# programming language  which means no linebreaks or indentation. Do not add any additional explanation" }
        };

        ThreadRun threadRun = assistantClient.CreateThreadAndRun(assistant.Id, threadOptions);
        do
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            threadRun = assistantClient.GetRun(threadRun.ThreadId, threadRun.Id);
        } while (!threadRun.Status.IsTerminal);
        
        CollectionResult<ThreadMessage> messages
            = assistantClient.GetMessages(threadRun.ThreadId, new MessageCollectionOptions() { Order = MessageCollectionOrder.Ascending });

        foreach (var threadMessage in messages)
        {
            var content = threadMessage.Content;
            if(threadMessage.Role.ToString().ToLower() == "assistant")
            {
                var jsonContent = threadMessage.Content.First().ToString();
                var formJsonArray = JArray.Parse(jsonContent);
                var result = new List<FormFieldDto>();
                foreach (var formField in formJsonArray)
                {
                    var fieldId = formField["field-id"].ToString();
                    var fieldLabel = formField["field-label"].ToString();
                    var inputType = formField["input-type"].ToString();
                    var requiredField = formField["required-field"].ToString();
                    var fieldOptions = formField["field-options"].ToString();
                    result.Add(new FormFieldDto(fieldId, fieldLabel, inputType, requiredField, fieldOptions));
                }
                return result;
            }
        }
        return new List<FormFieldDto>();
    }
}