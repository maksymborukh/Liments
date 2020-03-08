using Liments.MVC.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByAuthorAsync(string str);
        Task CreateAsync(Post item);
        Task UpdateAsync(Post item);
        Task DeleteAsync(string id);
    }
}
