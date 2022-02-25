namespace UnitTesting.Problems;

public class Animal
{
    public int AnimalSum(int a)
    {
        return a;
    }

    public int BasicSum(int a)
    {
        return a + 1;
    }
}

public class Mammal : Animal
{
    public int MammalSum(int a)
    {
        return base.BasicSum(a);
    }

    public new virtual int BasicSum(int a)
    {
        return a + 2;
    }
}

public class Dog : Mammal
{
    public int DogSum(int a)
    {
        return a;
    }

    public override int BasicSum(int a)
    {
        return a + 2;
    }
}

public class Beagle : Dog
{
    public int Sum(int a)
    {
        return base.MammalSum(a);
    }
}