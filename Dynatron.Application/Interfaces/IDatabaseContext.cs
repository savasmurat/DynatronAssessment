using Dynatron.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Application.Interfaces
{
    public interface IDatabaseContext
    {
        public DbSet<CustomerEntity> Customers { get; }

        Task AddAsync(object entity, CancellationToken cancellationToken);
        void Remove(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
