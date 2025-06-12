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
            var mockCarRepository = new Mock<ICarRepository>();
            mockUnitOfWork.Setup(u => u.Cars).Returns(mockCarRepository.Object);
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


        [Fact]
        public async Task GetAllAsync_ReturnsListOfCars()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCarRepository = new Mock<ICarRepository>();
            mockUnitOfWork.Setup(u => u.Cars).Returns(mockCarRepository.Object);
            var carService = new CarService(mockUnitOfWork.Object);
            var cars = new Car[] {
                new Car { Id = 1, Make = "TestMake", Model = "TestModel", Year = 2022 },
                new Car { Id = 2, Make = "TestMake2", Model = "TestModel2", Year = 2023 },
                new Car { Id = 3, Make = "TestMake3", Model = "TestModel3", Year = 2024 },
            };
            mockCarRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(cars);

            // Act
            var result = await carService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Length, cars.Length);
            Assert.Collection(result,
                item => Assert.Equal("TestMake", item.Make),
                item => Assert.Equal("TestMake2", item.Make),
                item => Assert.Equal("TestMake3", item.Make)
            );
        }


        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public async Task GetCarByIdAsync_WithValidAndInvalidId_ReturnsCar(int id, bool expected)
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCarRepository = new Mock<ICarRepository>();
            mockUnitOfWork.Setup(u => u.Cars).Returns(mockCarRepository.Object);
            var carService = new CarService(mockUnitOfWork.Object);
            var car = new Car() { Id = 1, Make = "TestMake", Model = "TestModel", Year = 2022 };
            mockCarRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(car);


            // Act
            Car? result;
            try
            {
                result = await carService.GetByIdAsync(id);
            }
            catch
            {
                await Assert.ThrowsAsync<ArgumentException>(
                    () => carService.GetByIdAsync(id));
                result = null;
            }

            // Assert
            Assert.Equal(expected, result != null);

        }

        [Fact]
        public async Task DeleteAsync_ValidId_ShouldDeleteCar()
        {
            // Arrange
            var carId = 1;
            var car = new Car { Id = carId, Make = "Toyota", Model = "Yaris", Year = 2015 };
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carService = new CarService(unitOfWorkMock.Object);
            unitOfWorkMock.Setup(u => u.Cars.GetByIdAsync(carId)).ReturnsAsync(car);
            unitOfWorkMock.Setup(u => u.Cars.DeleteAsync(car)).Returns(Task.CompletedTask);
            // Act
            await carService.DeleteAsync(carId);
            // Assert
            unitOfWorkMock.Verify(u => u.Cars.GetByIdAsync(carId), Times.Once);
            unitOfWorkMock.Verify(u => u.Cars.DeleteAsync(car), Times.Once);
        }
    }
}
