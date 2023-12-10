using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddressDto
{
    [Required]
    public string FirstName { get; init; }

    [Required]
    public string LastName { get; init; }

    [Required]
    public string Street { get; init; }

    [Required]
    public string City { get; init; }

    [Required]
    public string State { get; init; }

    [Required]
    public string ZipCode { get; init; }
}
