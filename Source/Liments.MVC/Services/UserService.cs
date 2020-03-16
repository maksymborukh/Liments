using AutoMapper;
using Liments.MVC.Core.Database;
using Liments.MVC.Core.Entities;
using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Liments.MVC.Services
{
    public class UserService : IUserService
    {
        private ILimentsContext _context;
        private IMapper _mapper;

        public UserService(ILimentsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var result = _mapper.Map<IEnumerable<UserViewModel>>(await _context.Users.AsQueryable().ToListAsync());
            return result;
        }

        public async Task<UserViewModel> GetByEmailAsync(string str)
        {
            var result = _mapper.Map<UserViewModel>(await _context.Users.Find(u => u.Email == str).SingleOrDefaultAsync());
            return result;
        }

        public UserViewModel GetByEmail(string str)
        {
            var result = _mapper.Map<UserViewModel>(_context.Users.Find(u => u.Email == str).SingleOrDefault());
            return result;
        }

        public async Task<UserViewModel> GetByUserNameAsync(string str)
        {
            var result = _mapper.Map<UserViewModel>(await _context.Users.Find(u => u.UserName == str).SingleOrDefaultAsync());
            return result;
        }

        public UserViewModel GetByUserName(string str)
        {
            var result = _mapper.Map<UserViewModel>(_context.Users.Find(u => u.UserName == str).SingleOrDefault());
            return result;
        }

        public async Task<bool> CheckCredentials(string name, string pass)
        {
            UserViewModel user = await GetByUserNameAsync(name);

            if (user != null && user.Password == GetHashCode(pass))
                return true;

            return false;
        }

        public async Task<bool> IsUserExist(string email, string name)
        {
            var user1 = await GetByEmailAsync(email);
            var user2 = await GetByUserNameAsync(name);

            if (user1 == null && user2 == null)
                return false;

            return true;
        }

        public async Task CreateAsync(UserViewModel user)
        {
            var nUser = _mapper.Map<User>(user);
            nUser.Password = GetHashCode(nUser.Password);
            await _context.Users.InsertOneAsync(nUser);
        }

        public async Task UpdateAsync(UserViewModel user)
        {
            var nUser = _mapper.Map<User>(user);
            await _context.Users.ReplaceOneAsync(u => u.Id == nUser.Id, nUser);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Users.DeleteOneAsync(u => u.Id == id);
        }

        private string GetHashCode(string pass)
        {
            var hasher = MD5.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(pass));
            var output = string.Empty;
            foreach (var b in hash)
            {
                output += b.ToString("X2");
            }

            return output;
        }
    }
}
