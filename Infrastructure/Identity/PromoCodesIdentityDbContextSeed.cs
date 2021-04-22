using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TheRoom.PromoCodes.Infrastructure.Identity
{
    public class PromoCodesIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new ApplicationUser { UserName = "promocodes@therooom.com", Email = "promocodes@therooom.com" };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
        }
    }
}
