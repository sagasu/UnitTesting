using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTesting.Account;
using UnitTesting.User;

namespace UnitTesting
{
    [TestClass]
    public class AccountProviderTests
    {
        [TestMethod]
        public void GetAccounts_ThatAreActive_OnlyActiveAccountsReturned()
        {
            //A
            const bool isActive = true;
            var depOne = new Mock<IDependencyOne>();
            var depTwo = new Mock<IDependencyTwo>();
            var dbProvider = new Mock<IDbProvider>();
            var userProvider = new UserProvider(depOne.Object, depTwo.Object, dbProvider.Object);
            var anyPositiveAmount = 2;
            var money = new Money(anyPositiveAmount);

            //A
            var activeAccounts = new AccountProvider().GetAccounts(userProvider, money, isActive);

            //A
            Assert.IsTrue(activeAccounts.All(x => x.IsActive));
        }

        [TestMethod]
        public void GetAccounts_ThatAreActive_OnlyActiveAccountsAreReturned()
        {
            const bool isActive = true;

            var activeAccounts = GetActiveAccounts(isActive);

            Assert.IsTrue(activeAccounts.All(x => x.IsActive));
        }

        private static IEnumerable<Account.Account> GetActiveAccounts(bool isActive)
        {
            var depOne = new Mock<IDependencyOne>();
            var depTwo = new Mock<IDependencyTwo>();
            var dbProvider = new Mock<IDbProvider>();
            var userProvider = new UserProvider(depOne.Object, depTwo.Object, dbProvider.Object);
            var anyPositiveAmount = 2;
            var money = new Money(anyPositiveAmount);

            var activeAccounts = new AccountProvider().GetAccounts(userProvider, money, isActive);
            return activeAccounts;
        }
    }
}

    
