using System;

namespace Van.Winkel.Financial.Contracts
{
    public class Account
    {
        public Guid CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}