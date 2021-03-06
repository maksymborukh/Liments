﻿using Liments.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByEmailAsync(string email);
        UserViewModel GetByEmail(string email);
        Task<UserViewModel> GetByIdAsync(string id);
        Task<UserViewModel> GetByUserNameAsync(string name);
        UserViewModel GetByUserName(string name);
        Task<bool> CheckCredentials(string login, string pass);
        Task<bool> IsUserExist(string email, string name);
        Task CreateAsync(UserViewModel item);
        Task UpdateAsync(string id, string fname, string lname, string email, string username, string pass);
        Task DeleteAsync(string id);
        Task FollowAsync(string username, string FolUsername);
        Task UnfollowAsync(string username, string FolUsername);
    }
}
