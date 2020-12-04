using System.Collections.Generic;
using System.Linq;

namespace UnitTesting
{
    public class AccountProvider
    {
        public IEnumerable<Account> GetAccounts(UserProvider userProvider, Money money, bool isActive)
        {
            return GetAccounts().Where(x => x.IsActive == isActive);
        }

        public IEnumerable<Account> GetAccounts()
        {
            yield return new Account(true);
            yield return new Account(true);
            yield return new Account(false);
            yield return new Account(false);
            yield return new Account(true);
        }
    }
}