using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Infrastructure.Database
{
    public abstract class AbstractRepository
    {
        private DbContext DbContext { get; }

        public AbstractRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            // You can implement additional logic before saving to database
            // Ex: Tracking some entities.
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
