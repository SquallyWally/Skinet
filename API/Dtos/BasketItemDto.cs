using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class BasketItemDto
{
    [Required]
    public int Id { get; init; }

    [Required]
    public string ProductName { get; init; }

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; init; }

    [Required]
    [Range(1, double.MaxValue,  ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; init; }

    [Required]
    public string PictureUrl { get; init; }

    [Required]
    public string Brand { get; init; }

    [Required]
    public string Type { get; init; }
}
