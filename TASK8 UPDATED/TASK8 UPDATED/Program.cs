using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;

var builder = WebApplication.CreateBuilder(args); //creates builder object

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //configures the db context to use sql server with connection string from appsettings.json

// Identity 
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); //adds identity services for user and role management, configures it to use the ApplicationDbContext for storing identity data, and adds default token providers for functionalities like password reset and email confirmation

builder.Services.AddControllersWithViews();  //adds support for controllers and views (MVC pattern)

var app = builder.Build();

// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    DbInitializer.SeedRolesAndUsers(userManager, roleManager); //calls the SeedRolesAndUsers method to ensure that the predefined roles and users are created in the database
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization(); //enables authentication and authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
