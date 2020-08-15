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
            if(string.IsNullOrWhiteSpace(name) || name.Length > 250)
                throw new InvalidOperationException("Name has incorrect length");
            Name = name;
            if (string.IsNullOrWhiteSpace(surname) || surname.Length > 250)
                throw new InvalidOperationException("Surname has incorrect length");
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
