using Microsoft.EntityFrameworkCore;
using Van.Winkel.Financial.Domain;

namespace Van.Winkel.Financial.Infrastructure.EntityFramework
{
    public class FinancialContext : DbContext, IFinancialContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypeConfigurationRegistration = new EntityTypeConfigurationRegistration(modelBuilder);
            entityTypeConfigurationRegistration.Register(GetType().Assembly);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
