using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SportStore.Infrastructure.Identity;

namespace SportStore.Infrastructure.Data;

public static class SeedData
{
    public static void InitializeUsers(UserManager<ApplicationUser> userManager)
    {
        SeedCatalogManager(userManager);
        SeedRegularUser(userManager);
    }

    private static void SeedCatalogManager(UserManager<ApplicationUser> userManager)
    {
        const string email = "manager@sportstore.com";
        const string password = "Manager123!";

        var user = userManager.FindByEmailAsync(email).Result;
        if (user is not null)
        {
            return;
        }

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = "Catalog",
            LastName = "Manager",
            BirthDate = new DateOnly(1990, 5, 12)
        };

        userManager.CreateAsync(user, password).Wait();
        userManager.AddClaimAsync(user, new Claim("CanManageCatalog", "true")).Wait();
    }

    private static void SeedRegularUser(UserManager<ApplicationUser> userManager)
    {
        const string email = "customer@sportstore.com";
        const string password = "Customer123!";

        var user = userManager.FindByEmailAsync(email).Result;
        if (user is not null)
        {
            return;
        }

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = "Regular",
            LastName = "Customer",
            BirthDate = new DateOnly(1996, 9, 3)
        };

        userManager.CreateAsync(user, password).Wait();
    }
}
