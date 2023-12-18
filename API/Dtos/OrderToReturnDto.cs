#nullable enable
namespace API.Dtos;
using Core.Entities.OrderAggregate;

public class OrderToReturnDto
{
    public int Id { get; init; }

    public string BuyerEmail { get; init; }

    public DateTime OrderDate { get; init; }

    public Address ShipToAddress { get; init; }

    public string DeliveryMethod { get; init; }

    public decimal ShippingPrice { get; init; }

    public IReadOnlyList<OrderItemDto> OrderItems { get; init; }

    public decimal Subtotal { get; init; }

    public decimal Total { get; init; }

    public string Status { get; init; }
}
