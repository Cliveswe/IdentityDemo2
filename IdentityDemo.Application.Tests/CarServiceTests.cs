using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using Moq;

namespace IdentityDemo.Application.Tests
{
    public class CarServiceTests
    {
        [Fact]
        public async Task AddAsync_WithCar()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var carService = new CarService(mockUnitOfWork.Object);

            var car = new Car()
            {
                Make = "TestMake",
                Model = "TestModel",
                Year = 2022
            };
            // Act
            await carService.AddAsync(car);


            // Assert
            mockUnitOfWork.Verify(u => u.Cars.AddAsync(It.Is<Car>
                (c => c.Make == "TestMake" && c.Model == "TestModel" && c.Year == 2022)), Times.Once);
        }
    }
}
