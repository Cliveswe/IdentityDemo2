using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using IdentityDemo.Web.Controllers;
using IdentityDemo.Web.Views.Car;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityDemo.Web.Tests;

public class CarControllerTests
{
    [Fact]
    public async Task Details_WithId_ReturnsIActionResult()
    {
        // Arrange
        var car = new Car { Id = 1, Make = "TestMake", Model = "TestModel", Year = 2022 };
        var mockCarService = new Mock<ICarService>();
        var carController = new CarController(mockCarService.Object);

        mockCarService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(car);

        // Act
        var result = await carController.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<DetailsVM>(viewResult.Model);
        Assert.Equal(car.Make, model.Make);
        Assert.Equal(car.Model, model.Model);
        Assert.Equal(car.Year, model.Year);
        mockCarService.Verify(s => s.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task AllDetails_ReturnsIActionResult()
    {
        // Arrange
        var cars = new Car[]
        {
           new Car { Id = 1, Make = "TestMake1", Model = "TestModel1", Year = 2020 },
           new Car { Id = 2, Make = "TestMake2", Model = "TestModel2", Year = 2021 },
           new Car { Id = 3, Make = "TestMake3", Model = "TestModel3", Year = 2022 }
        };

        var mockCarService = new Mock<ICarService>();
        var carController = new CarController(mockCarService.Object);

        mockCarService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(cars);

        // Act
        var result = await carController.AllDetails();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<AllDetailsVM>(viewResult.Model);
        Assert.NotNull(model.CarVMs);
        Assert.Equal(model.CarVMs.Length, cars.Length);
        Assert.Collection(model.CarVMs,
            item => Assert.Equal("TestMake1", item.Make),
            item => Assert.Equal("TestMake2", item.Make),
            item => Assert.Equal("TestMake3", item.Make));
        mockCarService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Create_WithViewModel_ReturnsIActionResult()
    {
        // Arrange
        var carVM = new CreateVM { Make = "TestMake", Model = "TestModel", Year = 2022 };

        var mockCarService = new Mock<ICarService>();
        var carController = new CarController(mockCarService.Object);

        var car = new Car() { Make = carVM.Make, Model = carVM.Model, Year = carVM.Year };

        mockCarService.Setup(s => s.AddAsync(car));

        // Act
        var result = await carController.Create(carVM);

        // Assert

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
        mockCarService.Verify(s => s.AddAsync(It.Is<Car>
            (c => c.Make == carVM.Make && c.Model == carVM.Model && c.Year == carVM.Year)), Times.Once);
    }
}
