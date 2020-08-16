using System;
using System.Collections.Generic;

namespace Van.Winkel.Financial.Contracts
{
    public class CustomerWithAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<AccountWithTransactions> Accounts { get; set; }
    }
}