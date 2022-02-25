using System.Collections.Generic;
using System.Linq;

namespace UnitTesting.User
{
    public interface IUserProvider
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsers(bool isActive);
        IEnumerable<User> GetAllUsers(bool isActive,bool isHaveMoney, bool isOld);
        IEnumerable<User> GetAllUsersFromDb();
    }

    public class UserProvider : IUserProvider
    {
        private readonly IDbProvider _three;
        public UserProvider(IDependencyOne one, IDependencyTwo two, IDbProvider three)
        {
            _three = three;
        }

        public IEnumerable<UnitTesting.User.User> GetAllUsers()
        {
            yield return new User(true, true, false);
            yield return new User(true, false, true);
            yield return new User(true, true, true);
            yield return new User(false, false, false);
            yield return new User(false, true, true);
            yield return new User(false, true, true);
            yield return new User(true, true, true);
        }

        public IEnumerable<User> GetAllUsers(bool isActive)
        {
            return GetAllUsers().Where(x => x.IsActive == isActive);
        }

        public IEnumerable<User> GetAllUsers(bool isActive,bool isHaveMoney, bool isOld)
        {
            return GetAllUsers().Where(x => x.IsActive == isActive && x.IsHaveMoney == isHaveMoney && x.IsOld == isOld);
        }

        public IEnumerable<User> GetAllUsersFromDb()
        {
            yield return new User(true, true, false);
        }

    }
}