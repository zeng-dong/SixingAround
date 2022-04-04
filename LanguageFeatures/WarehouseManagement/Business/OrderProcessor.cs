using WarehouseManagement.Domain;

namespace WarehouseManagement.Business;

public class OrderProcessor
{
    public delegate void OrderInitialized(Order order);

    // instead of expose a public property, this one will
    // only be invoked by this class: see the Process() method
    public delegate void ProcessCompleted(Order order);

    public OrderInitialized? OnOrderInitialized { get; set; }

    private void Initialize(Order order)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));

        OnOrderInitialized?.Invoke(order);

        ////if (OnOrderInitialized is not null)
        ////    OnOrderInitialized();
    }

    public void Process(Order order, ProcessCompleted onCompleted = default)
    {
        Initialize(order);
        onCompleted?.Invoke(order);
    }
}