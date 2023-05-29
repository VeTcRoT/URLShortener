using Microsoft.AspNetCore.Identity;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public static class Seed
    {
        public async static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var userStore = serviceProvider.GetRequiredService<IUserStore<IdentityUser>>();

            var roles = Enum.GetValues(typeof(Roles)).Cast<Roles>().ToList();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                    if (role == Roles.Admin)
                    {
                        var user = new IdentityUser();
                        await userStore.SetUserNameAsync(user, "admin", CancellationToken.None);
                        var result = await userManager.CreateAsync(user, "Admin2232622!");

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role.ToString());
                        }
                    }
                }
            }
        }
    }
}
