using IdentityDemo.Infrastructure.Persistence;
using IdentityDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IdentityDemo.Infrastructure.Persistence;

namespace IdentityDemo.Terminal;

internal class Program
{
    static CarService carService = null!;
    static async Task Main()
    {
        string? connectionString;

        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", false);
        var app = builder.Build();
        connectionString = app.GetConnectionString("DefaultConnection");

        // Configure DbContext options
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(connectionString)
            .Options;

        // Create and use the context
        var context = new ApplicationContext(options);
        var carRepository = new CarRepository(context);
        carService = new(new UnitOfWork(context, carRepository));

        await ListAllCarsAsync();
    }

    private static async Task ListAllCarsAsync()
    {
        foreach (var item in await carService.GetAllAsync())
        {
            Console.WriteLine(item.Make);
            Console.WriteLine(item.Model);
            Console.WriteLine(item.Year);
            Console.WriteLine("");
        }

        Console.WriteLine("------------------------------");
    }
}
