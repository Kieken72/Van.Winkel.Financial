using System;
using Xunit;

namespace Van.Winkel.Financial.Domain.Tests
{
    public class AccountTests
    {

        [Fact]
        public void When_CreatingNewCorrectAccount_Expect_CorrectAccount()
        {

            var account = new Account(Guid.Empty);

            Assert.Equal(Guid.Empty, account.CustomerId);
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void When_UpdatingBalanceEqualToZero_Expect_InvalidOperationException()
        {
            var account = new Account(Guid.Empty);
            Assert.Throws<InvalidOperationException>(()=> account.UpdateBalance(0));
        }

        [Fact]
        public void When_UpdatingBalanceUnderZero_Expect_InvalidOperationException()
        {
            var account = new Account(Guid.Empty);
            Assert.Throws<InvalidOperationException>(() => account.UpdateBalance(-1));
        }

        [Fact]
        public void When_UpdatingBalanceAboveZero_Expect_CorrectAccount()
        {
            var account = new Account(Guid.Empty);
            account.UpdateBalance(1);
            Assert.Equal(1, account.Balance);
        }
    }
}
