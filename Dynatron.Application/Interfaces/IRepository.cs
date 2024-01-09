using Dynatron.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Application.Interfaces
{
    public interface IRepository
    {
        public DbSet<CustomerEntity> Customers { get; }

        Task AddAsync<TEntity>([NotNull] TEntity entity, CancellationToken cancellationToken) where TEntity : class;
        void Remove<TEntity>([NotNull] TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
