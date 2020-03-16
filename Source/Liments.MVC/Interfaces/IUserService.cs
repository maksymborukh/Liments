using Liments.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByEmailAsync(string email);
        UserViewModel GetByEmail(string email);
        Task<UserViewModel> GetByUserNameAsync(string name);
        UserViewModel GetByUserName(string name);

        Task<bool> CheckCredentials(string login, string pass);
        Task<bool> IsUserExist(string email, string name);
        Task CreateAsync(UserViewModel item);
        Task UpdateAsync(UserViewModel item);
        Task DeleteAsync(string id);
    }
}
