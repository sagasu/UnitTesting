using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    [TestClass]
    public class UserProviderTests
    {
        [TestMethod]
        public void GetAllUsers_ThatAreActive_OnlyActiveUsersReturned()
        {
            const bool isActive = true;

            var activeUsers = new UserProvider().GetAllUsers(isActive);

            Assert.IsTrue(activeUsers.All(x => x.IsActive));
        }

        [TestMethod]
        public void GetAllUsers_ThatAreActiveHaveMoneyAndAreOld_OnlyActiveWithMoneyOldUsersReturned()
        {
            const bool isActive = true; 
            const bool isHaveMoney = true; 
            const bool isOld = true; 

            var activeUsers = new UserProvider().GetAllUsers(isActive, isHaveMoney, isOld);

            Assert.IsTrue(activeUsers.All(IsActiveHaveMoneyIsOld));
        }

        private bool IsActiveHaveMoneyIsOld(User user)
        {
            return user.IsActive && user.IsHaveMoney && user.IsOld;
        }
    }
}
