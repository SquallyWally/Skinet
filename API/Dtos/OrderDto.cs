namespace API.Dtos;

public class OrderDto
{
    public string BasketId { get; init; }

    public int DeliveryMethodId { get; init; }

    public AddressDto ShipToAddress { get; init; }
}
