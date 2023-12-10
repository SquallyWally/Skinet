using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; init; }

    public List<BasketItemDto> BasketItems { get; init; } = new();
}
