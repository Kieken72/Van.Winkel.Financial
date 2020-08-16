using Microsoft.EntityFrameworkCore;
using Van.Winkel.Financial.Domain;

namespace Van.Winkel.Financial.Infrastructure.EntityFramework
{
    public class FinancialContext : DbContext, IFinancialContext
    {
        public FinancialContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.RegisteTypeConfigurations(GetType().Assembly);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
