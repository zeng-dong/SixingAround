using WarehouseManagement.Domain;

namespace WarehouseManagement.Business;

public class OrderProcessor
{
    public delegate void OrderInitialized();

    public OrderInitialized? OnOrderInitialized { get; set; }

    private void Initialize(Order order)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));

        OnOrderInitialized?.Invoke();

        //if (OnOrderInitialized is not null)
        //{
        //    OnOrderInitialized();
        //}
    }

    public void Process(Order order)
    {
        Initialize(order);
    }
}