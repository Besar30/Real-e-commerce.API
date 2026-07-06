using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Real_e_commerce.API.Middleware;
using Real_e_commerce.API.SignalR;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Data;
using Real_e_commerce.Infrastructure.Data.SeedingData;
using Real_e_commerce.Infrastructure.Repositories;
using Real_e_commerce.Infrastructure.Services;
using Real_e_commerce.Infrastructure.Setting;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Host.UseSerilog((context, configration) =>
configration.ReadFrom.Configuration(context.Configuration));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("https://localhost:4200", "https://localhost:7135")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connString = builder.Configuration.GetConnectionString("Redis");
    if (connString == null) throw new Exception("cannot get radis connection string");
    var configration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configration);
});
builder.Services.AddSingleton<ICartServices, CartServices>();
builder.Services.AddScoped<IPaymentServices,PaymentServices>();
builder.Services.AddSingleton<IResponseCacheService, ResponseCacheService>();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>()
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("StripeSetting"));
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
app.MapHub<NotificationHub>("/hub/notifications");
try
{
    using var scope = app.Services.CreateScope();
    var services= scope.ServiceProvider;
    var context= services.GetRequiredService<ApplicationDbContext>();
    var userManger = services.GetRequiredService<UserManager<AppUser>>();

    await context.Database.MigrateAsync();
    await ApplicationContextSeed.SeedAsync(context, userManger);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    throw;
}
app.Run();
