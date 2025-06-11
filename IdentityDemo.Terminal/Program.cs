using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using IdentityDemo.Extensions;
using IdentityDemo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityDemo.Terminal;

internal class Program
{
    static CarService carService = null!;
    static async Task Main() {
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

        // Start the application
        Start();

    }

    /// <summary>
    /// Starts the main application loop, displaying a menu and handling user input.
    /// </summary>
    /// <remarks>This method runs an infinite loop that presents a menu to the user, processes their input, 
    /// and performs actions based on the selected option. The loop continues until the user chooses  to exit the
    /// application.</remarks>
    private static void Start() {
        while(true) {
            DisplayMenu();
            string? input = Console.ReadLine();
            switch(input) {
                case "1":
                ListAllCarsAsync().GetAwaiter().GetResult();
                break;
                case "2":
                GetCarByID().GetAwaiter().GetResult();
                break;
                case "0":
                Environment.Exit(0);
                break;
                default:
                "Invalid option. Please try again.".DisplayErrorMessage();
                break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Displays the main menu for the Car Management System.
    /// </summary>
    /// <remarks>Clears the console and displays a formatted menu with options for listing all cars, 
    /// retrieving a car by its ID, or exiting the application. This method is intended to  provide a user-friendly
    /// interface for navigating the system.</remarks>
    private static void DisplayMenu() {
        Console.Clear();
        "Car Management System".DisplayBoldText();
        "\n------------------------------".DisplayInfoMessage();
        "1. List All Cars".DisplayStandardMessage();
        "2. Get Car By ID".DisplayStandardMessage();
        "0. Exit".DisplayStandardMessage();
        "\n------------------------------".DisplayInfoMessage();
    }

    /// <summary>
    /// Retrieves and displays the details of a car based on the provided car ID.
    /// </summary>
    /// <remarks>Prompts the user to enter a car ID, validates the input, and attempts to retrieve the car
    /// details using the car service. If the car is found, its details are displayed. If the car is not found or an
    /// invalid ID is provided, an appropriate error message is displayed.</remarks>
    /// <returns></returns>
    private static async Task GetCarByID() {

        "Enter Car ID: ".DisplayTitle();
        var input = Console.ReadLine();

        if(int.TryParse(input, out int id)) {
            try {
                var car = await carService.GetByIdAsync(id);

                if(car is null) {
                    "Car not found.".DisplayErrorMessage();
                    return;
                }

                DisplayCarDetails(car);

            } catch(ArgumentException ex) {
                $"Could not find the car with id: {id}".DisplayErrorMessage();
                ex.Message.DisplayErrorMessage();
            }
        }
        else {
            "Invalid ID format. Please enter a valid integer.".DisplayErrorMessage();
        }
    }

    /// <summary>
    /// Asynchronously retrieves and displays a list of all cars.
    /// </summary>
    /// <remarks>This method fetches all car records using the car service and displays their details. The
    /// output includes a formatted header and footer for better readability.</remarks>
    /// <returns></returns>
    private static async Task ListAllCarsAsync() {

        "List of Cars".DisplayBoldText();
        "\n------------------------------".DisplayInfoMessage();

        foreach(Car item in await carService.GetAllAsync()) {

            DisplayCarDetails(item);
            Console.WriteLine("");
        }

        "------------------------------".DisplayInfoMessage();
    }

    /// <summary>
    /// Displays the details of the specified car in a formatted manner.
    /// </summary>
    /// <remarks>This method outputs the car's ID, make, model, and year to the console, with each detail
    /// formatted for readability. Ensure that the <paramref name="car"/> parameter is not null before calling this
    /// method.</remarks>
    /// <param name="car">The <see cref="Car"/> object whose details are to be displayed. Cannot be null.</param>
    private static void DisplayCarDetails(Car car) {
        "Car Details:".DisplaySuccessMessage();
        Console.WriteLine();
        "ID   : ".DisplayTitle();
        car.Id.DisplayStandardMessage();
        "Make : ".DisplayTitle();
        car.Make.DisplayStandardMessage();
        "Model: ".DisplayTitle();
        car.Model.DisplayStandardMessage();
        "Year : ".DisplayTitle();
        car.Year.DisplayStandardMessage();
    }
}
