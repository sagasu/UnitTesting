using System.Collections;
using System.Collections.Generic;

namespace UnitTesting.User;

public interface IDependencyOne
{
}

public interface IDependencyTwo
{
}

public interface IDbProvider
{
}

public class DbProvider: IDbProvider
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
}