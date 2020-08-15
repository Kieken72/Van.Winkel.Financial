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

    public class Transaction
    {

        public Guid Id { get; private set; }
        public Guid RecipientAccountId { get; private set; }
        public Guid SenderAccountId { get; private set; }
        public string Note { get; private set; }
        public decimal Amount { get; private set; }
    }
}
