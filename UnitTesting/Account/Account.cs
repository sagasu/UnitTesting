namespace UnitTesting.Account;

public class Account
{
    public bool IsActive { get; }

    public Account(bool isActive)
    {
        IsActive = isActive;
    }
}