using Liments.MVC.Core.Entities;
using MongoDB.Driver;

namespace Liments.MVC.Core.Database
{
    public interface ILimentsContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Post> Posts { get; }
    }
}
