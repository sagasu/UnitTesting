namespace UnitTesting
{
    public class User
    {
        public bool IsActive { get; }
        public bool IsHaveMoney { get; }
        public bool IsOld { get; }

        public User(bool isActive, bool isHaveMoney, bool isOld)
        {
            IsActive = isActive;
            IsHaveMoney = isHaveMoney;
            IsOld = isOld;
        }
    }
}