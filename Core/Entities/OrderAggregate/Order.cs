namespace Core.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public Order()
    {
    }

    public Order(
        IReadOnlyList<OrderItem> orderItems,
        string                   buyerEmail,
        Address                  shipToAddress,
        DeliveryMethod           deliveryMethod,
        decimal                  subtotal)
    {
        OrderItems = orderItems;
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        Subtotal = subtotal;
    }

    public IReadOnlyList<OrderItem> OrderItems { get; init; }

    public string BuyerEmail { get; init; }

    public DateTime OrderDate { get; init; } = DateTime.Now;

    public Address ShipToAddress { get; init; }

    public DeliveryMethod DeliveryMethod { get; init; }

    public decimal Subtotal { get; init; }

    public OrderStatus Status { get; init; } = OrderStatus.Pending;

    public string PaymentIntentId { get; init; }

    public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
}
