namespace WarehouseManagement;

public static class FuncCompose
{
    public static void Run()
    {
        var vals = new int[] { 1, 2, 3, 4, 5 };

        Func<int, int> inc = e => e + 1;
        Func<int, int> cube = e => e * e * e;

        var res = vals.Select(inc).Select(cube);

        foreach (var e in res)
        {
            Console.WriteLine(e);
        }
    }
}