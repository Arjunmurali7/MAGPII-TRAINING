var builder = WebApplication.CreateBuilder(args); //create a builder object

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //add swagger services to the container
builder.Services.AddSwaggerGen(); //add swagger generator to the container

var app = builder.Build();//build the app

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) ///check if the environment is development
{
    app.UseSwagger();//add swagger middleware to the pipeline
    app.UseSwaggerUI();//add swagger ui to the pipeline
}

app.UseHttpsRedirection(); //redirect http requests to https

app.UseAuthorization(); //add authorization middleware to the pipeline

app.MapControllers(); //map the controllers to the endpoints

app.Run();
