using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using IdentityDemo.Web.Controllers;
using IdentityDemo.Web.Views.Car;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityDemo.Web.Tests
{
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
    }
}
