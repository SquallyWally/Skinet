using System.ComponentModel.DataAnnotations;

using Core.Entities.Identity;

public class Address
{
    public int Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Street { get; init; }

    public string City { get; init; }

    public string State { get; init; }

    public string ZipCode { get; init; }

    [Required]
    public string AppUserId { get; init; }

    public AppUser AppUser { get; init; }
}
