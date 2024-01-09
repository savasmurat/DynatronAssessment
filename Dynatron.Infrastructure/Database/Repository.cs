using Dynatron.Application.Entities;
using Dynatron.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Infrastructure.Database
{
    public class Repository : AbstractRepository, IRepository
    {
        private IDatabaseContext DatabaseContext { get; }
        public Repository(DatabaseContext databaseContext): base(databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public DbSet<CustomerEntity> Customers => DatabaseContext.Customers;

        public async Task AddAsync<TEntity>([NotNull] TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            await DatabaseContext.AddAsync(entity, cancellationToken);
        }

        public void Remove<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            DatabaseContext.Remove(entity);
        }
    }
}
