using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);// Create builder

builder.Services.AddControllersWithViews();// Add services to the container.

// MySQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"), //reads from appsettings.json
        new MySqlServerVersion(new Version(8, 0, 36)) 
    ));

var app = builder.Build();// Build the app

app.UseStaticFiles(); // Enable static files like CSS, JS, images
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");// Default route

app.Run();
