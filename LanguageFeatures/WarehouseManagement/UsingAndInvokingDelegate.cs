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

        new OrderProcessor
        {
            OnOrderInitialized = SendMessageToWarehouse
        }.Process(order, SendConfirmationEmail);
    }

    static bool SendMessageToWarehouse(Order order)
    {
        Console.WriteLine($"Please pack the order for {order.OrderNumber} - {string.Concat(order.LineItems.Select(x => x.Name))}");
        return true;
    }

    static void SendConfirmationEmail(Order order)
    {
        Console.WriteLine($"Order comfirmed for {order.OrderNumber} --- {string.Concat(order.LineItems.Select(x => x.Name))}");
    }
}