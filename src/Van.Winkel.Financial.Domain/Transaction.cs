using System;

namespace Van.Winkel.Financial.Domain
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid RecipientAccountId { get; private set; }
        public virtual Account RecipientAccount { get; private set; }
        public Guid SenderAccountId { get; private set; }
        public virtual Account SenderAccount { get; private set; }
        public string Note { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(Guid recipientAccountId, Guid senderAccountId, decimal amount, string note = null)
        {
            RecipientAccountId = recipientAccountId;
            SenderAccountId = senderAccountId;
            Note = note;
            Amount = amount;
            Date = DateTime.UtcNow;
        }

        //Empty ctor for EF
        public Transaction() { }
    }
}