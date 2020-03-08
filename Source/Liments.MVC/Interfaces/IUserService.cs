using Liments.MVC.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string uName);
        Task CreateAsync(User item);
        Task UpdateAsync(User item);
        Task DeleteAsync(string id);
    }
}
