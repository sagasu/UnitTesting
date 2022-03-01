using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTesting.Problems;
using UnitTesting.Problems.Injection;
using UnitTesting.Problems.Property;
using UnitTesting.User;

namespace UnitTesting
{
    [TestClass]
    public class UserProviderTests
    {
        

        // This is an example of something that shouldn't be tested, because we are testing flow/state and not behaviour.
        [TestMethod]
        public void ShortestPathLength_ShortestPathLenghtIsExtracted_ShortestPathLengthIsCalled()
        {
            var shortestPathService = new Mock<IShortestPathService>();
            var anyGraph = new int[2][];
            var anyPositiveInteger = 1;
            shortestPathService.Setup(CalculateShortestPath()).Returns(anyPositiveInteger);
            var shortestPathVisitingAllNodes = new ShortestPathVisitingAllNodesGarbage(shortestPathService.Object);

            shortestPathVisitingAllNodes.ShortestPathLength(anyGraph);

            shortestPathService.Verify(CalculateShortestPath(), Times.Once);
        }

        // This is an example of something that shouldn't be tested, because we are testing flow/state and not behaviour.
        [TestMethod]
        public void ShortestPathLength_hortestPathLenghtIsExtracted_ShortestPathLengthIsCalled()
        {
            var garbageProperty = new ShortestPathVisitingAllNodesGarbageProperty();
            var anyGraph = new int[2][];

            garbageProperty.ShortestPathLength(anyGraph);
            Assert.IsNotNull(garbageProperty.Candidates);
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

        // Nothing is tested here.
        [TestMethod]
        public void ShortestPathLength_NotTestingAnything_NothingIsTested()
        {
            var users = new Mock<IShortest_Path_Visiting_All_Nodes_Garbage_Tests>();

            var someGraph = new int[2][];
            users.Setup(provider => provider.ShortestPathLength(someGraph)).Returns(1);

            users.Object.ShortestPathLength(someGraph);

            users.Verify(provider => provider.CalculateShortestPath(someGraph, new List<int>()), Times.Once);
        }

        private static Expression<Func<IShortestPathService, int>> CalculateShortestPath()
        {
            var anyGraph = new int[2][];
            var anyList = new List<int>();
            var anyParentChild = new Dictionary<int, int>();
            var anyDicGraph = new Dictionary<int, List<int>>();
            return pathService => pathService.CalculateShortestPath(anyGraph, anyList, anyDicGraph, anyParentChild);
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
