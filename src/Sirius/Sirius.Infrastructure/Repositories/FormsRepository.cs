using Sirius.Application.Forms;
using Sirius.Core.Entities;
using Sirius.Infrastructure.Data;

namespace Sirius.Infrastructure.Repositories;

public class FormsRepository(ApplicationDbContext context) : BaseRepository<Form>(context), IFormsRepository
{
    
}