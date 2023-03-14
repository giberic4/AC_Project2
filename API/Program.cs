<<<<<<< HEAD
using Models;
using Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


=======
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DataAccess;
using Services;
using Models;
>>>>>>> 9ba8360d55189e9966d4423ebbdaeb6cf0e9004b

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UserServices>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

// AddSingleton => The same instance is shared across the entire app over the lifetime of the application
// AddScoped => The instance is created every new request
// AddTransient => The instance is created every single time it is required as a dependency 

builder.Services.AddScoped<IRepository, DBRepository>(ctx => new DBRepository(builder.Configuration.GetConnectionString("P2DB")));
builder.Services.AddScoped<UserServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/login", ([FromBody] User user, UserServices service) => {
    return service.UserLogin(user);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

<<<<<<< HEAD
app.MapPost("/users/createAccount", ([FromBody] User user, UserServices service) => {
    return "User Created: " + Results.Created("/users", service.CreateAccount(user));
});
=======

>>>>>>> 9ba8360d55189e9966d4423ebbdaeb6cf0e9004b

app.Run();
