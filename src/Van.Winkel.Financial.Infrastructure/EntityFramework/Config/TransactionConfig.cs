using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Van.Winkel.Financial.Domain;

namespace Van.Winkel.Financial.Infrastructure.EntityFramework.Config
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            var table = builder.ToTable("Transaction");
            table.HasKey("Id");
        }

    }
}