using Liments.MVC.Core.Database;
using Liments.MVC.Core.Entities;
using Liments.MVC.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liments.MVC.Services
{
    public class UserService : IUserService
    {
        private ILimentsContext _context;

        public UserService(ILimentsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.AsQueryable().ToListAsync();
            return result;
        }

        public async Task<User> GetByEmailAsync(string str)
        {
            var result = await _context.Users.Find(u => u.Email == str).SingleOrDefaultAsync();
            return result;
        }

        public async Task<User> GetByUserNameAsync(string str)
        {
            var result = await _context.Users.Find(u => u.UserName == str).SingleOrDefaultAsync();
            return result;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Users.DeleteOneAsync(u => u.Id == id);
        }
    }
}
