using System;
using System.Collections.Generic;

namespace Van.Winkel.Financial.Domain
{
    public class Customer
    {
        public Guid Id  { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public virtual ICollection<Account> Accounts { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Customer(string name, string surname)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            CreatedOn = DateTime.UtcNow;
        }

        public void ChangeName(string name, string surname)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
            if (!string.IsNullOrWhiteSpace(surname))
                Surname = surname;
        }

        //Empty ctor for EF
        public Customer() { }
    }
}
