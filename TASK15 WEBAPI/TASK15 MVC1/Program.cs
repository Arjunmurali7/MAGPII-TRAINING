var builder = WebApplication.CreateBuilder(args); //create a builder object
builder.Services.AddControllersWithViews(); //add mvc services to the container

builder.Services.AddHttpClient("CalculatorApi", client => //configure the httpclient instance
{
    client.BaseAddress = new Uri("https://localhost:7199/"); //set the base address of the api
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CalculatorClient}/{action=Index}/{id?}"); //set the default route to the controller and action
app.Run();