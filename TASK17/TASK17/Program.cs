var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();// Add services to the container.
var app = builder.Build();

if (!app.Environment.IsDevelopment())// Configure the HTTP request pipeline.
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();