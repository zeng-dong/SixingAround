namespace WarehouseManagement.Domain;

public class ShippingProvider
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal FreightCost { get; set; }
}