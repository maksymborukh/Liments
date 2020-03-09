using Liments.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByEmailAsync(string email);
        Task<UserViewModel> GetByLoginAsync(string login);
        Task<bool> CheckCredentials(string login, string pass);
        Task<bool> IsUserExist(string email);
        Task CreateAsync(UserViewModel item);
        Task UpdateAsync(UserViewModel item);
        Task DeleteAsync(string id);
    }
}
