using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EmployeeMgmt.Data.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            
            string[] roles = { "Admin", "Manager", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation($"✅ Role '{role}' created.");
                }
            }

            
            await EnsureUserAsync(userManager, logger, "admin@tak7.com", "Admin@123", "Admin");
            await EnsureUserAsync(userManager, logger, "manager@tak7.com", "Manager@123", "Manager");
            await EnsureUserAsync(userManager, logger, "employee@tak7.com", "Employee@123", "Employee");
        }

        private static async Task EnsureUserAsync(UserManager<IdentityUser> userManager, ILogger logger, string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(user, password);
                if (createResult.Succeeded)
                {
                    logger.LogInformation($"✅ User '{email}' created.");
                }
                else
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    logger.LogError($"❌ Failed to create user '{email}': {errors}");
                    return;
                }
            }
            else
            {
                
                await userManager.RemovePasswordAsync(user);
                var resetResult = await userManager.AddPasswordAsync(user, password);
                if (resetResult.Succeeded)
                {
                    logger.LogInformation($"🔑 Password for '{email}' reset.");
                }
                else
                {
                    var errors = string.Join(", ", resetResult.Errors.Select(e => e.Description));
                    logger.LogError($"❌ Failed to reset password for '{email}': {errors}");
                }
            }

            
            if (!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
                logger.LogInformation($"➡️ User '{email}' added to role '{role}'.");
            }
        }
    }
}
