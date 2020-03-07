using Liments.DAL.Entities;

namespace Liments.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Post> Posts { get; }
    }
}
