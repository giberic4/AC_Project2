
using Models;
using Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://example.com",
                                              "http://www.contoso.com",
                                              "http://localhost:4200",
                                              "http://localhost:5144")
                                              .AllowAnyHeader();
                      });
});

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

// app.MapPost("/user-inventory", ([FromQuery] int userid, UserServices service) => {
//     User user = new User();
//     user.Id=userid; 
//     return service.ViewPersonalInventory(user).listOfItems;
// });

app.MapGet("/user-inventory/userid", ([FromQuery] int userid, UserServices service) => {
    User user = new User();
    user.Id=userid;
    return service.ViewPersonalInventory(user).listOfItems;
});

app.MapGet("/user", ([FromQuery] int userid, UserServices service) => {
    return service.GetUserByID(userid);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapPost("/users/createAccount", ([FromBody] User user, UserServices service) => {
    return "User Created: " + Results.Created("/users", service.CreateAccount(user));
});

app.Run();
