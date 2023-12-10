namespace Core.Entities;

public class CustomerBasket
{
    public CustomerBasket(
        string id)
    {
        Id = id;
    }

#pragma warning disable CS8618 //
    public CustomerBasket()
#pragma warning restore CS8618 // 
    {
    }

    public string Id { get; init; }

    public List<BasketItem> BasketItems { get; init; } = new();
}
