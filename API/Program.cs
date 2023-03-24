using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models;
using Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text;
using JWTWeb;
using Serilog;
using Microsoft.AspNetCore.HttpLogging;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("../Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

// Add services to the container.

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<ItemServices>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

// AddSingleton => The same instance is shared across the entire app over the lifetime of the application
// AddScoped => The instance is created every new request
// AddTransient => The instance is created every single time it is required as a dependency 

builder.Services.AddScoped<IRepository, DBRepository>(ctx => new DBRepository(builder.Configuration.GetConnectionString("P2DB")));
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<ItemServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});



var app = builder.Build();

app.UseStaticFiles();

app.UseHttpLogging();

app.MapPost("/login", ([FromBody] User user, UserServices service) => {
    try{
        Log.Information("User attempting login...");
        return service.UserLogin(user);
    }
    catch (Exception ex){
        Log.Error("Error! Something fatal happened with login", ex);
        throw;
    }
});


app.MapGet("/user-inventory/userid", ([FromQuery] int userid, UserServices service) => {
    try{
        Log.Information("Viewing user inventory...");
        User user = new User();
        user.Id=userid;
        return service.ViewPersonalInventory(user).listOfItems;
    }
    catch(Exception){
        Log.Error("Error! Something fatal happened with user inventory");
        throw;
    }
});


app.MapGet("/hello", () => {
    try{
        Log.Information("Saying hello...");
        return "Hello";
    }
    catch(Exception ex){
        Log.Error("Something Fatal happened when attempting to say hello", ex);
        throw;
    }
});


app.MapGet("/user", ([FromQuery] int userid, UserServices service) => {
    try{
        Log.Information("Attempting to track user by id...");
        return service.GetUserByID(userid);
    }
    catch(Exception ex){
        Log.Error("Something fatal happened when getting user by id", ex);
        throw;
    }
});


    app.MapGet("/user1", ([FromQuery] string username, UserServices service) => {
    try{
        Log.Information("Getting user by username");
        return service.GetUserByUsername(username);
    }
    catch(Exception ex){
        Log.Error("Something fatal happened when retrieving user via username", ex);
        throw;
    }
});


app.MapGet("/marketplace", (UserServices service) => {
    try{
        Log.Information("Attempting to view marketplace");
        return service.GetMarketplaceItems();
    }
    catch(Exception ex){
        Log.Error("Something fatal happened when viewing marketplace", ex);
        throw;
    }
});


app.MapGet("/marketplaceByName", (string searchitem, UserServices service) => {
    try{
        Log.Information("Getting market items by name");
        return service.getMarketplaceItemsByName(searchitem);
    }
    catch(Exception ex){
        Log.Error("Something fatal happened when getting market items by name", ex);
        throw;
    }
});


app.MapPost("/users/createAccount", ([FromBody] User user, UserServices service) => {
    try{
        Log.Information("Attempting to create account");
        return Results.Created("/users/createAccount", service.CreateAccount(user));
        }
    catch(Exception ex){
        Log.Error("Something fatal happened when creating account", ex);
        throw;
    }
});


app.MapPost("store/buy/", ([FromBody] Misc intarr, ItemServices service) => {
    try{
        Log.Information("Attempting to buy item");
        service.buyItem(intarr);
    }
    catch(Exception ex){
        Log.Information("Something fatal happened when buying item", ex);
        throw;
    }
});


app.MapPost("grabbag", ([FromBody] int num, ItemServices service) => {
    try{
        Log.Information("Using the grab bag");
        return service.buy_rand(num);
    }
    catch(Exception ex){
        Log.Error("Something fatal happened when using grab bag", ex);
        throw;
    }
});



app.MapPost("store/sell/", ([FromBody] Sellinfo intarr, ItemServices service) => {
        try{
            Log.Information("Attempting to sell items");
            Console.WriteLine("AAAAAAAAAAAA",intarr);
            service.sellItem(intarr);
        }
        catch(Exception ex){
            Log.Error("Something fatal happened when selling item", ex);
            throw;
        }
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();



app.Run();
