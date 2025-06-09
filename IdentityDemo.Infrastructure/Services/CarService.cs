using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityDemo.Infrastructure.Services
{
    public class CarService : ICarService
    {
        public Task AddAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public Task<Car[]> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Car?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
