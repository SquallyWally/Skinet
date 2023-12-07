using Core.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(
        UserManager<AppUser> userManager)
    {
        if ( !userManager.Users.Any() )
        {
            var user = new AppUser
                {
                    DisplayName = "Cloud",
                    Email = "cloud_strife@test.com",
                    UserName = "cloud_strife@test.com",
                    Address = new Address
                        {
                            FirstName = "Cloud",
                            LastName = "Strife",
                            Street = "Midgar Ave 100",
                            City = "Nibelheim",
                            State = "MG",
                            ZipCode = "54643",
                        }
                };

            await userManager.CreateAsync(
                user,
                "P@$$w0rd");
        }
    }
}
