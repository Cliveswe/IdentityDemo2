using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityDemo.Infrastructure.Persistence
{
    public class CarRepository(ApplicationContext context) : ICarRepository
    {
        public async Task AddAsync(Car car)
        {
            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();
        }

        public async Task<Car[]> GetAllAsync() =>
            await context.Cars.AsNoTracking().ToArrayAsync();

        public async Task<Car?> GetByIdAsync(int id) => await context.Cars
            .FindAsync(id);

        public async Task DeleteAsync(Car car)
        {
            if (car is not null)
            {
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
            }
        }
    }
}
