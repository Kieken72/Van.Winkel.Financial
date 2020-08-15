using System;
using System.Collections;
using System.Collections.Generic;

namespace Van.Winkel.Financial.Domain
{
    public class Account
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }
        public decimal Balance { get; private set; }
        public DateTime OpenedOn { get; private set; }
        public virtual ICollection<Transaction> IncomingTransactions { get; private set; }
        public virtual ICollection<Transaction> OutgoingTransactions { get; private set; }

        public Account(Guid customerId)
        {
            CustomerId = customerId;
            OpenedOn = DateTime.UtcNow;
        }

        public void UpdateBalance(decimal amount)
        {
            if (Balance + amount <= 0)
                throw new InvalidOperationException("Balance cannot be less then zero.");
            Balance += amount;
        }

        //Empty ctor for EF
        public Account() { }
    }
}