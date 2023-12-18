using Core.Entities.OrderAggregate;

namespace Core.Interfaces;

using Address = Entities.OrderAggregate.Address;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(
        string          buyerEmail,
        int             deliveryMethodId,
        string          basketId,
        Address shippingAddress);

    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(
        string buyerEmail);

    Task<Order?> GetOrderByIdAsync(
        int    id,
        string buyerEmail);

    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}
