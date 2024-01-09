using Dynatron.Application.Entities;
using Dynatron.Application.Interfaces;
using Dynatron.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Infrastructure.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customers { get; private set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        }

        public async new Task AddAsync(object entity, CancellationToken cancellationToken) => await base.AddAsync(entity, cancellationToken);

        public new void Remove(object entity) => base.Remove(entity);
    }
}
