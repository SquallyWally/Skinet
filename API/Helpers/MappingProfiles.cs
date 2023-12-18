using API.Dtos;

using AutoMapper;

using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductsToReturnDto>()
            .ForMember(
                d => d.ProductBrand,
                o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(
                d => d.ProductType,
                o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(
                d => d.PictureUrl,
                o => o.MapFrom<ProductUrlResolver>());

        CreateMap<Address, AddressDto>()
            .ReverseMap();

        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();
        CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

        CreateMap<Order, OrderToReturnDto>()
            .ForMember(
                o => o.DeliveryMethod,
                o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(
                o => o.ShippingPrice,
                o => o.MapFrom(s => s.DeliveryMethod.Price));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(
                o => o.ProductName,
                o => o.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(
                o => o.ProductId,
                o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(
                o => o.PictureUrl,
                o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
            .ForMember(
                d => d.PictureUrl,
                o => o.MapFrom<OrderItemUrlResolver>());
    }
}
