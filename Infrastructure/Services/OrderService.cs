using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

using OrderAddress = Core.Entities.OrderAggregate.Address;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepository;

    private readonly IGenericRepository<DeliveryMethod> _deliveryRepository;

    private readonly IGenericRepository<Product> _productsRepository;

    private readonly IBasketRepository _basketRepository;

    public OrderService(
        IGenericRepository<Order>          orderRepository,
        IGenericRepository<DeliveryMethod> deliveryRepository,
        IGenericRepository<Product>        productsRepository,
        IBasketRepository                  basketRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
    }

    public async Task<Order> CreateOrderAsync(
        string       buyerEmail,
        int          deliveryMethodId,
        string       basketId,
        OrderAddress shippingAddress)
    {
        // get basket from repo
        var retrievedBasket = await _basketRepository.GetBasketAsync(basketId);

        //grab products items
        var orderItems = new List<OrderItem>();

        foreach ( var item in retrievedBasket.BasketItems )
        {
            var productItem = await _productsRepository.GetByIdAsync(item.Id);

            var itemOrdered = new ProductItemOrdered(
                productItem.Id,
                productItem.Name,
                productItem.PictureUrl);

            var orderItem = new OrderItem(
                itemOrdered,
                item.Price,
                item.Quantity);

            orderItems.Add(orderItem);
        }
        
        // get delivery method from repo
        var deliveryItem = await _deliveryRepository.GetByIdAsync(deliveryMethodId);

        //get subtotal
        var subtotal = orderItems.Sum(item => item.Price);

        // create order
        var order = new Order(
            orderItems,
            buyerEmail,
            shippingAddress,
            deliveryItem,
            subtotal);

        // save 

        return order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(
        string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetOrderByIdAsync(
        int    id,
        string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        throw new NotImplementedException();
    }
}
