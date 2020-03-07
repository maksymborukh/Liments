using Liments.DAL.Entities;
using MongoDB.Driver;

namespace Liments.DAL.Core
{
    public interface ILimentsContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Post> Posts { get; }
    }
}
