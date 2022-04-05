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

        OrderProcessor.ProcessCompleted chain = StepOne;
        chain += StepTwo;
        chain += StepThree;

        //new OrderProcessor
        //{
        //    OnOrderInitialized = SendMessageToWarehouse
        //}.Process(order, SendConfirmationEmail);

        var processor = new OrderProcessor { OnOrderInitialized = SendMessageToWarehouse };
        processor.Process(order, SendConfirmationEmail);
        processor.Process(order, chain);
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

    static void StepOne(Order order) => Console.WriteLine("Step one");

    static void StepTwo(Order order) => Console.WriteLine("Step two");

    static void StepThree(Order order) => Console.WriteLine("Step three");
}