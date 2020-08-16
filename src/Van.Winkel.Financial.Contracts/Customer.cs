using System;
using System.Collections.Generic;
using System.Text;

namespace Van.Winkel.Financial.Contracts
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class CustomerWithAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<AccountWithTransactions> Accounts { get; set; }
    }
    public class AccountWithTransactions
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }

    public class OpenAccount
    {
        public decimal InitialCredit { get; set; }

    }


    public class Account
    {
        public Guid CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
    public class Transaction
    {
        public Guid? SenderAccountId { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
    }
}
