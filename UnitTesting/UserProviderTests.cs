using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTesting.User;

namespace UnitTesting
{
    [TestClass]
    public class UserProviderTests
    {
        [TestMethod]
        public void GetAllUsers_ThatAreActive_OnlyActiveUsersReturned()
        {
            const bool isActive = true;

            var activeUsers = GetUserProvider().GetAllUsers(isActive);

            Assert.IsTrue(activeUsers.All(x => x.IsActive));
        }

        [TestMethod]
        public void GetAllUsers_ThatAreActiveHaveMoneyAndAreOld_OnlyActiveWithMoneyOldUsersReturned()
        {
            const bool isActive = true; 
            const bool isHaveMoney = true; 
            const bool isOld = true; 

            var activeUsers = GetUserProvider().GetAllUsers(isActive, isHaveMoney, isOld);

            Assert.IsTrue(activeUsers.All(IsActiveHaveMoneyIsOld));
        }

        [TestMethod]
        public void GetAllUsersFromDB_DefaultConfig_CallsDBProvider()
        {
            var users = new Mock<IUserProvider>();
            users.Setup(provider => provider.GetAllUsersFromDb()).Returns(new List<User.User>());
            
            users.Object.GetAllUsersFromDb();

            users.Verify(provider => provider.GetAllUsersFromDb(), Times.Once);
        }

        private bool IsActiveHaveMoneyIsOld(User.User user)
        {
            return user.IsActive && user.IsHaveMoney && user.IsOld;
        }

        private static UserProvider GetUserProvider()
        {
            var depOne = new Mock<IDependencyOne>();
            var depTwo = new Mock<IDependencyTwo>();
            var depThree = new Mock<IDbProvider>();
            return new UserProvider(depOne.Object, depTwo.Object, depThree.Object);
        }
    }
}
