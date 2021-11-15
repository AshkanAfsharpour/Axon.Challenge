using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Axon.Domain.Entities;

namespace Axon.Application.Common.Interfaces
{
    public interface IAxonContext
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
