using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityDemo.Infrastructure.Persistence;

namespace IdentityDemo.Infrastructure.Services
{
    public class CarService(IUnitOfWork unitOfWork) : ICarService
    {
        public async Task AddAsync(Car car)
        {
            await unitOfWork.Cars.AddAsync(car);
        }

        public async Task<Car[]> GetAllAsync()
        {
            var cars = await unitOfWork.Cars.GetAllAsync();
            return [.. cars.OrderBy(c => c.Make)];
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            var car = await unitOfWork.Cars.GetByIdAsync(id);

            return car is null ?
                throw new ArgumentException($"Invalid parameter value: {id}", nameof(id)) :
                car;
        }
    }
}
