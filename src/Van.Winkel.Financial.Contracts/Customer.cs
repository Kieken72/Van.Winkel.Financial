﻿using System;
using System.Text;

namespace Van.Winkel.Financial.Contracts
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
