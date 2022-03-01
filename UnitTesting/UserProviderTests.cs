using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTesting.Problems;
using UnitTesting.Problems.Injection;
using UnitTesting.User;

namespace UnitTesting
{
    [TestClass]
    public class UserProviderTests
    {
        [TestMethod]
        public void GetAllUsersFromDB_DefaultConfig_CallsDBProvider2()
        {
            var users = new Mock<IShortest_Path_Visiting_All_Nodes_Garbage_Tests>();

            var someGraph = new int[2][];
            users.Setup(provider => provider.ShortestPathLength(someGraph)).Returns(1);

            users.Object.ShortestPathLength(someGraph);

            users.Verify(provider => provider.CalculateShortestPath(someGraph, new List<int>()), Times.Once);
        }

        // This is an example of something that shouldn't be tested, because we are testing flow/state and not behaviour.
        [TestMethod]
        public void ShortestPathLength_ShortestPathLenghtIsExtracted_ShortestPathLengthIsCalled()
        {
            var shortestPathService = new Mock<IShortestPathService>();
            
            var anyGraph = new int[2][];
            var anyList = new List<int>();
            var anyParentChild = new Dictionary<int, int>();
            var anyDicGraph = new Dictionary<int, List<int>>();
            var anyPositiveInteger = 1;

            shortestPathService.Setup(pathService => pathService.CalculateShortestPath(anyGraph, anyList, anyDicGraph, anyParentChild)).Returns(anyPositiveInteger);
            var shortestPathVisitingAllNodes = new ShortestPathVisitingAllNodesGarbage(shortestPathService.Object);

            shortestPathVisitingAllNodes.ShortestPathLength(anyGraph);

            shortestPathService.Verify(provider => provider.CalculateShortestPath(anyGraph, anyList, anyDicGraph, anyParentChild), Times.Once);
        }
        
        [TestMethod]
        public void GetAllUsersFromDB_DefaultConfig_CallsDBProvider4()
        {
            var users = new Mock<IShortest_Path_Visiting_All_Nodes_Garbage_Tests>();

            var someGraph = new int[2][];
            var anyList = new List<int>();

            users.Setup(provider => provider.ShortestPathLength(someGraph)).Returns(1);
            users.Setup(provider => provider.CalculateShortestPath(someGraph, anyList)).Returns(1);

            users.Object.ShortestPathLength(someGraph);
            users.Object.CalculateShortestPath(someGraph,anyList);

            users.Verify(provider => provider.CalculateShortestPath(someGraph, anyList), Times.Once);
        }

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
