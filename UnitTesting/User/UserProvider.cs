using System.Collections.Generic;
using System.Linq;

namespace UnitTesting
{
    public class UserProvider
    {
        public IEnumerable<User> GetAllUsers()
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
    }
}