using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityDemo.Application.Cars;

namespace IdentityDemo.Infrastructure.Persistence
{
    public class UnitOfWork(
        ApplicationContext context,
        ICarRepository carRepository) : IUnitOfWork
    {
        public ICarRepository Cars => carRepository;

        public async Task PersistAllAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
