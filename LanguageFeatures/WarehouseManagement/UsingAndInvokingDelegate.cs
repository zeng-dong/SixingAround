using WarehouseManagement.Business;
using WarehouseManagement.Domain;

namespace WarehouseManagement;

public class UsingAndInvokingDelegate
{
    public static void Run()
    {
        var order = new Order
        {
            LineItems = new[]
            {
                new Item{ Name = "PS1", Price = 50 },
                new Item{ Name = "PS2", Price = 60 },
                new Item{ Name = "PS3", Price = 70 },
                new Item{ Name = "PS4", Price = 80 },
            }
        };

        var processor = new OrderProcessor();

        processor.OnOrderInitialized = SendMessageToWarehouse;

        processor.Process(order);
    }

    static void SendMessageToWarehouse()
    {
        Console.WriteLine("Please pack the order");
    }

    static void SendConfirmationEmail()
    {
        Console.WriteLine("Order comfirmed");
    }
}