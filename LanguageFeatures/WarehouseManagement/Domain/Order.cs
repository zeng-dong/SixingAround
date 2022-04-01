namespace WarehouseManagement.Domain;

public class Order
{
    public Guid OrderNumber { get; init; }
    public ShippingProvider ShippingProvider { get; init; }
    public int Total { get; }
    public bool IsReadyForShipment { get; set; } = true;
    public IEnumerable<Item> LineItems { get; set; }

    public Order()
    {
        OrderNumber = Guid.NewGuid();
    }
}

public class Item
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}