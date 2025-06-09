using IdentityDemo.Application.Cars;

namespace IdentityDemo.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        ICarRepository Cars { get; }
        Task PersistAllAsync();
    }
}