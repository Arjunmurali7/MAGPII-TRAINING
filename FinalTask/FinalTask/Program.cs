using EmployeeAttendanceAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);// create a web application builder

builder.Services.AddControllers();// add controllers to the services container
builder.Services.AddDbContext<AppDbContext>(options =>// add the database context to the services container
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"), // get the connection string from appsettings.json
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))// automatically detect the server version
    )
);
builder.Services.AddEndpointsApiExplorer(); // add endpoints api explorer to the services container
builder.Services.AddSwaggerGen();// add swagger generator to the services container

var app = builder.Build();// build the web application

if (app.Environment.IsDevelopment()) // if the environment is development
{
    app.UseSwagger();// use swagger middleware
    app.UseSwaggerUI();// use swagger ui middleware
}

app.UseHttpsRedirection();// use https redirection middleware
app.UseAuthorization();//use authorization middleware
app.MapControllers();// map controllers to the endpoints
app.Run();
