using IdentityDemo.Domain.Entities;

namespace IdentityDemo.Application.Cars
{
    public class CarService(IUnitOfWork unitOfWork) : ICarService
    {
        public async Task AddAsync(Car car) =>
            await unitOfWork.Cars.AddAsync(car);


        public async Task<Car[]> GetAllAsync()
        {
            var cars = await unitOfWork.Cars.GetAllAsync();
            return [.. cars.OrderBy(c => c.Make)];
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            var car = await unitOfWork.Cars.GetByIdAsync(id);

            return car is null ?
                throw new ArgumentException($"Invalid parameter value: {id}", nameof(id)) :
                car;
        }

        public async Task DeleteAsync(int id)
        {
            var car = await GetByIdAsync(id);
            if (car is null)
                throw new ArgumentException($"Invalid parameter value: {id}", nameof(id));
            await unitOfWork.Cars.DeleteAsync(car);
        }
    }
}
