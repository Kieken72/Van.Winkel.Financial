using System;
using Xunit;

namespace Van.Winkel.Financial.Domain.Tests
{
    public class CustomerTests
    {

        private const string Name = "Jef";
        private const string Surname = "Kortleven";

        private const string UpdatedName = "Jean";
        private const string UpdatedSurname = "Langleven";

        [Fact]
        public void When_CreatingNewCorrectCustomer_Expect_CorrectCustomer()
        {
            var customer = new Customer(Name, Surname);

            Assert.Equal(Name, customer.Name);
            Assert.Equal(Surname, customer.Surname);
        }

        [Fact]
        public void When_CreatingNewCustomerWithEmptyName_Expect_InvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(()=>new Customer("", Surname));
        }

        [Fact]
        public void When_CreatingNewCustomerWithEmptySurname_Expect_InvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new Customer(Name, ""));
        }

        [Fact]
        public void When_CreatingNewCustomerWithEmptyNames_Expect_InvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new Customer("", ""));
        }

        [Fact]
        public void When_ChangingCustomerNameWithValidName_Expect_UpdatedCustomer()
        {
            var customer = new Customer(Name, Surname);

            customer.ChangeName(UpdatedName, UpdatedSurname);

            Assert.Equal(UpdatedName, customer.Name);
            Assert.Equal(UpdatedSurname, customer.Surname);
        }

        [Fact]
        public void When_ChangingCustomerNameWithInvalidName_Expect_UpdatedCustomerExceptName()
        {
            var customer = new Customer(Name, Surname);

            customer.ChangeName("", UpdatedSurname);

            Assert.Equal(Name, customer.Name);
            Assert.Equal(UpdatedSurname, customer.Surname);
        }

        [Fact]
        public void When_ChangingCustomerNameWithInvalidSurname_Expect_UpdatedCustomerExceptSurname()
        {
            var customer = new Customer(Name, Surname);

            customer.ChangeName(UpdatedName, "");

            Assert.Equal(UpdatedName, customer.Name);
            Assert.Equal(Surname, customer.Surname);
        }

        [Fact]
        public void When_ChangingCustomerNameWithInvalidNames_Expect_NotUpdatedCustomer()
        {
            var customer = new Customer(Name, Surname);

            customer.ChangeName("", "");

            Assert.Equal(Name, customer.Name);
            Assert.Equal(Surname, customer.Surname);
        }
    }
}
