using Dynatron.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// I normally use DataAnootations in Entity class.
    /// This is to demonstrate entity configuration for advanced features.
    /// </summary>
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.FirstName).HasColumnType("nvarchar(50)");
            builder.Property(x => x.LastName).HasColumnType("nvarchar(50)");
            builder.Property(x => x.Email).HasColumnType("varchar(50)");
            builder.Property(x => x.LastUpdatedDateTime).IsRequired(false);
        }
    }
}
