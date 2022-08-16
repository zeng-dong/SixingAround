namespace WarehouseManagement;

internal static class PassingFuncs
{
    private static int Add(int x) => x + x;

    private static int Add(Func<int, int> how, int original) => how(original) + how(original);

    private static int Time(int x) => x * x;

    ///  how can we do func of func:
    ///private static int Strange(Func<Func<int, int>, int> how, int original)
    ///{
    ///    ///var x = how(original);
    ///
    ///    return how(Add, original);
    ///}

    private static int Execute(Func<int, int> how, int original) => how(original);

    internal static void Run()
    {
        Console.WriteLine(Execute(Add, 3));
        Console.WriteLine(Execute(Time, 3));

        Console.WriteLine(Execute(Time, Execute(Add, 2)));

        Console.WriteLine(Add(Add, 3));
        Console.WriteLine(Add(Time, 3));
    }
}