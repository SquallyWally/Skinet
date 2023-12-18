using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

using OrderAddress = Core.Entities.OrderAggregate.Address;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IBasketRepository _basketRepository;

    public OrderService(
        IUnitOfWork       unitOfWork,
        IBasketRepository basketRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

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
            var productItem = await _unitOfWork.Repository<Product>()
                .GetByIdAsync(item.Id);

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

        var deliveryItem = await _unitOfWork.Repository<DeliveryMethod>()
            .GetByIdAsync(deliveryMethodId);

        //get subtotal
        var subtotal = orderItems.Sum(item => item.Price);

        // create order
        var order = new Order(
            orderItems,
            buyerEmail,
            shippingAddress,
            deliveryItem,
            subtotal);

        _unitOfWork.Repository<Order>()
            .Add(order);

        // save 
        var result = await _unitOfWork.Complete();

        if ( result <= 0 )
            return null;

        // delete basket
        await _basketRepository.DeleteBasketAsync(basketId);

        return order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(
        string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);

        return await _unitOfWork.Repository<Order>()
            .ListAsync(spec);
    }

    public async Task<Order?> GetOrderByIdAsync(
        int    id,
        string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(
            id,
            buyerEmail);

        return await _unitOfWork.Repository<Order>()
            .GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync() =>
        await _unitOfWork.Repository<DeliveryMethod>()
            .ListAllAsync();
}
