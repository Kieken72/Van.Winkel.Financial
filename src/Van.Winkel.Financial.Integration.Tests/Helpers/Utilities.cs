using System;
using System.Collections.Generic;
using Van.Winkel.Financial.Domain;
using Van.Winkel.Financial.Infrastructure.EntityFramework;

namespace Van.Winkel.Financial.Integration.Tests.Helpers
{
    public static class Utilities
    {

        public static Guid CustomerId;
        public static void InitializeDbForTests(FinancialContext db)
        {
            db.Customers.AddRange(GetSeedingCustomers());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(FinancialContext db)
        {
            db.Customers.RemoveRange(db.Customers);
            InitializeDbForTests(db);
        }

        public static List<Customer> GetSeedingCustomers()
        {
            var customer = new Customer("Jef","Kortleven");

            CustomerId = customer.Id;

            return new List<Customer>()
            {
                customer,
            };
        }
    }
}