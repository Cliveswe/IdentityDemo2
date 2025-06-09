using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityDemo.Domain.Entities;

namespace IdentityDemo.Application.Cars
{
    public interface ICarRepository
    {
        Task AddAsync(Car car);
        Task<Car[]> GetAllAsync();

        Task<Car?> GetByIdAsync(int id);
    }
}
