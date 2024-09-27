using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using Sirius.Application.Forms;
using Sirius.Application.Interfaces;

namespace Sirius.Infrastructure.Services;

public class MlService(IConfiguration configuration) : IMlService
{
    public async Task<IEnumerable<FormFieldDto>> GetFormFieldsFromSourceCode(string sourceCode)
    {
        var openaiApiKey = configuration["OpenAI:ApiKey"];
        ChatClient client = new(model: "gpt-3.5-turbo", openaiApiKey);

        ChatCompletion chatCompletion = await client.CompleteChatAsync(
        [
            new UserChatMessage("Tell me how you feel today"),
        ]);

        var result = chatCompletion.ToString();


        return new List<FormFieldDto>()
        {
            new FormFieldDto
            ("TestId", "TestLabel", "TestType", result, "TestPlaceholder", "TestValidation", "TestOptions"),
        };
    }
}