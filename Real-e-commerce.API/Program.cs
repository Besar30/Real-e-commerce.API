using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Data;
using Real_e_commerce.Infrastructure.Data.SeedingData;
using Real_e_commerce.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    using var scope = app.Services.CreateScope();
    var services= scope.ServiceProvider;
    var context= services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    await ApplicationContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    throw;
}
app.Run();
