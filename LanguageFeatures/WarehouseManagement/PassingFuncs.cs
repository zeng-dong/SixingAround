namespace WarehouseManagement;

internal static class PassingFuncs
{
    private static int Add(int x) => x + x;

    private static int Time(int x) => x * x;

    private static int Execute(Func<int, int> how, int original) => how(original);

    internal static void Run()
    {
        Console.WriteLine(Execute(Add, 3));
        Console.WriteLine(Execute(Time, 3));
    }
}