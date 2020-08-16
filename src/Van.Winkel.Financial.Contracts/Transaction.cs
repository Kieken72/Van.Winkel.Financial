using System;

namespace Van.Winkel.Financial.Contracts
{
    public class Transaction
    {
        public Guid? SenderAccountId { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
    }
}