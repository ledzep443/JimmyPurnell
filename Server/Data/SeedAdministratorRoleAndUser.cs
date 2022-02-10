using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Server.Data
{
    public static class SeedAdministratorRoleAndUser
    {
        internal const string AdministratorRoleName = "Administrator";
        internal const string AdministratorUserName = "jimmypurnell27@gmail.com";

        internal async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            await SeedAdministratorRole(roleManager);
            await SeedAdministratorUser(userManager);
        }

        private async static Task SeedAdministratorRole(RoleManager<IdentityRole> roleManager)
        {
            bool administratorRoleExists = await roleManager.RoleExistsAsync(AdministratorRoleName);

            if (administratorRoleExists == false)
            {
                var role = new IdentityRole
                {
                    Name = AdministratorRoleName
                };

                await roleManager.CreateAsync(role);
            }
        }

        private async static Task SeedAdministratorUser(UserManager<IdentityUser> userManager)
        {
            bool administratorUserExists = await userManager.FindByEmailAsync(AdministratorUserName) != null;

            if (administratorUserExists == false)
            {
                var user = new IdentityUser
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName
                };

                //Make sure to change the password to something secure on first run. This password is meant to be temporary
                IdentityResult result = await userManager.CreateAsync(user, "Password1!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AdministratorRoleName);
                }
            }
        }
    }
}
