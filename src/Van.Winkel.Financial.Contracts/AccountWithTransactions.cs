using System;
using System.Collections.Generic;

namespace Van.Winkel.Financial.Contracts
{
    public class AccountWithTransactions
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}