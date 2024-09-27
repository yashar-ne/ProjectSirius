using Microsoft.EntityFrameworkCore;
using Sirius.Core.Entities;

namespace Sirius.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Form> Forms { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}