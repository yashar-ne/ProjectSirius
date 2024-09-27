using Sirius.Application.Forms;

namespace Sirius.Application.Interfaces;

public interface IMlService
{
    Task<IEnumerable<FormFieldDto>> GetFormFieldsFromSourceCode(string sourceCode);
}