using IdentityDemo.Domain.Entities;

namespace IdentityDemo.Application.Cars
{
    public interface ICarService
    {
        Task AddAsync(Car car);
        Task<Car[]> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
