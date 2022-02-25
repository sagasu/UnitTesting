namespace UnitTesting.Problems;

public static class Singleton
{
    public static int Start()
    {
        return Stage1(2);
    }

    private static int Stage1(int a)
    {
        return Stage2(a, 2);
    }

    private static int Stage2(in int a, int b)
    {
        return IAmALeafMethod(a, b);
    }

    public static int IAmALeafMethod(int a, int b)
    {
        return a + b;
    }
}