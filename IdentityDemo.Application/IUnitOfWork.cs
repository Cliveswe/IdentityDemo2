using IdentityDemo.Application.Cars;

namespace IdentityDemo.Application
{
    public interface IUnitOfWork
    {
        ICarRepository Cars { get; }
        Task PersistAllAsync();
    }
}