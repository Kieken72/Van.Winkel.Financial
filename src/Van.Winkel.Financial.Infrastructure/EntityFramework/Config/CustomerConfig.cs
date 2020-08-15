using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Van.Winkel.Financial.Domain;

namespace Van.Winkel.Financial.Infrastructure.EntityFramework.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            var table = builder.ToTable("Customer");
            table.HasKey("Id");
            table.HasMany(_ => _.Accounts).WithOne(_ => _.Customer).HasForeignKey(_ => _.CustomerId);
        }

    }
}
