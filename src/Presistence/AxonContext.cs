using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Axon.Application.Common.Interfaces;
using Axon.Domain.Entities;
using Axon.Domain.Common;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Axon.Presistence
{
    public partial class AxonContext : DbContext, IAxonContext
    {
        private readonly IApplicationUserService _applicationUserService;

        public AxonContext()
        {
        }

        public AxonContext(DbContextOptions<AxonContext> options,
            IApplicationUserService applicationUserService)
            : base(options)
        {
            _applicationUserService = applicationUserService;
            
        }
        public virtual DbSet<GitHubProfile> GitHubProfiles { get; set; }
        public virtual DbSet<GitHubRepository> GitHubRepositories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=Axon;Integrated Security=true; Max Pool Size =1000;Connect Timeout=50;ConnectRetryCount=10");
            }
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = entry.Entity.Id == default ? Guid.NewGuid() : entry.Entity.Id;
                        entry.Entity.CreatedBy = _applicationUserService?.Id;
                        entry.Entity.CreatedAt = entry.Entity.CreatedAt == default ? DateTime.UtcNow : entry.Entity.CreatedAt;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _applicationUserService?.Id;
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);


        }
    }
}
