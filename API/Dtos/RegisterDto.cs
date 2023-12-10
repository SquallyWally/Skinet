using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    public string DisplayName { get; init; }

    [Required]
    [RegularExpression(
        @"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}",
        ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non-alphanumeric and at least 7 characters")]
    public string Password { get; init; }
}
