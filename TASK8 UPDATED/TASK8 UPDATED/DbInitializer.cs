using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Data
{
    public static class DbInitializer
    {
        public static void SeedRolesAndUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) // Seed roles and users
        {
            // Roles
            string[] roles = { "Admin", "Manager", "Employee" }; //creates role if not
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            // Admin
            if (userManager.FindByEmailAsync("admin@example.com").Result == null)
            {
                var admin = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
                userManager.CreateAsync(admin, "Admin123!").Wait();
                userManager.AddToRoleAsync(admin, "Admin").Wait();
            }

            // Manager
            if (userManager.FindByEmailAsync("manager@example.com").Result == null)
            {
                var manager = new IdentityUser { UserName = "manager@example.com", Email = "manager@example.com" };
                userManager.CreateAsync(manager, "Manager123!").Wait();
                userManager.AddToRoleAsync(manager, "Manager").Wait();
            }

            // Employee
            if (userManager.FindByEmailAsync("employee@example.com").Result == null)
            {
                var employee = new IdentityUser { UserName = "employee@example.com", Email = "employee@example.com" };
                userManager.CreateAsync(employee, "Employee123!").Wait();
                userManager.AddToRoleAsync(employee, "Employee").Wait();
            }
        }
    }
}
