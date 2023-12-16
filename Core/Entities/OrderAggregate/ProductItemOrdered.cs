namespace Core.Entities.OrderAggregate;

public class ProductItemOrdered
{
    public ProductItemOrdered()
    {
    }

    public ProductItemOrdered(
        int    productItemId,
        string productName,
        string pictureUrl)
    {
        ProductItemId = productItemId;
        ProductName = productName;
        PictureUrl = pictureUrl;
    }

    public int ProductItemId { get; init; }

    public string ProductName { get; init; }

    public string PictureUrl { get; init; }
}
