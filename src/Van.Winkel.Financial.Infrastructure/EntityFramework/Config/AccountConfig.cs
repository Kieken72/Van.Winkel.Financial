using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Van.Winkel.Financial.Domain;

namespace Van.Winkel.Financial.Infrastructure.EntityFramework.Config
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            var table = builder.ToTable("Account");
            table.HasKey("Id");
            table.HasMany(_ => _.IncomingTransactions).WithOne(_ => _.RecipientAccount).HasForeignKey(_ => _.RecipientAccountId);
            table.HasMany(_ => _.OutgoingTransactions).WithOne(_ => _.SenderAccount).HasForeignKey(_ => _.SenderAccountId);
        }

    }
}