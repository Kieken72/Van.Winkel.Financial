using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Van.Winkel.Financial.Domain;

namespace Van.Winkel.Financial.Infrastructure.EntityFramework
{
    public interface IFinancialContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Transaction> Transactions { get; }
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}