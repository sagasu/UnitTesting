using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    [TestClass]
    public class AccountProviderTests
    {
        [TestMethod]
        public void GetAllAccounts_ThatAreActive_OnlyActiveAccountsReturned()
        {
            //A
            const bool isActive = true;
            var userProvider = new UserProvider();
            var anyAmount = 2;
            var money = new Money(anyAmount);

            //A
            var activeAccounts = new AccountProvider().GetAccounts(userProvider, money, isActive);

            //A
            Assert.IsTrue(activeAccounts.All(x => x.IsActive));
        }

        [TestMethod]
        public void GetAllAccounts_ThatAreActive_OnlyActiveAccountsAreReturned()
        {
            const bool isActive = true;

            var activeAccounts = GetActiveAccounts(isActive);

            Assert.IsTrue(activeAccounts.All(x => x.IsActive));
        }

        private static IEnumerable<Account> GetActiveAccounts(bool isActive)
        {
            var userProvider = new UserProvider();
            var anyPositiveAmount = 2;
            var money = new Money(anyPositiveAmount);

            var activeAccounts = new AccountProvider().GetAccounts(userProvider, money, isActive);
            return activeAccounts;
        }
    }
}

    
