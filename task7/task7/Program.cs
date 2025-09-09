using Microsoft.EntityFrameworkCore;
using task7.Data;

var builder = WebApplication.CreateBuilder(args);

// Force HTTP in Docker
builder.WebHost.UseUrls("http://+:80");

// Add DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // API

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Comment out HTTPS redirection for Docker
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

// API route
app.MapControllers();

app.Run();
