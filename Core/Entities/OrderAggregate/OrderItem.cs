namespace Core.Entities.OrderAggregate;

public class OrderItem : BaseEntity
{
    public OrderItem()
    {
    }

    public OrderItem(
        ProductItemOrdered itemOrdered,
        decimal            price,
        int                quantity)
    {
        ItemOrdered = itemOrdered;
        Price = price;
        Quantity = quantity;
    }

    public ProductItemOrdered ItemOrdered { get; init; }

    public decimal Price { get; init; }

    public int Quantity { get; init; }
}
