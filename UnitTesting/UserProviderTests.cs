using System.Collections.Generic;
using System.Linq;
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
        public void GetAllUsers_ThatAreOld_OnlyOldUsersReturned()
        {
            const bool isOld = true;

            var oldUsers = GetUsers(isOld);

            Assert.IsTrue(oldUsers.All(x => x.IsOld));
        }


        [TestMethod]
        public void GetOldestUser_OldestUserDefined_ReturnsOldestUser()
        {
            var oldestUser = GetUserProvider().GetOldestUser();

            Assert.IsTrue(IsHomomorphicEqual(UserProvider.OldestUser, oldestUser));
        }

        [TestMethod]
        public void GetAllUsersFromDB_DefaultConfig_CallsDBProvider()
        {
            var users = new Mock<IUserProvider>();
            users.Setup(provider => provider.GetAllUsersFromDb()).Returns(new List<User.User>());
            
            users.Object.GetAllUsersFromDb();

            users.Verify(provider => provider.GetAllUsersFromDb(), Times.Once);
        }

        private IEnumerable<User.User> GetUsers(bool isOld)
        {
            return GetUserProvider().GetAllUsers().Where(x=>x.IsOld == isOld);
        }

        private bool IsHomomorphicEqual(User.User a, User.User b)
        {
            return a.IsActive == b.IsActive && a.IsHaveMoney == b.IsHaveMoney && a.IsOld == b.IsOld;
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
