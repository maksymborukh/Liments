using Liments.DAL.Core;
using Liments.DAL.Entities;
using Liments.DAL.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liments.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private LimentsContext _context;

        public UserRepository(LimentsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.AsQueryable().ToListAsync();
            return result;
        }

        public async Task<User> GetAsync(string str)
        {
            var result = await _context.Users.Find(u => u.Email == str).SingleOrDefaultAsync();
            return result;
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            var result = _context.Users.AsQueryable().Where(predicate).ToList();
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

        public async Task UpdateFieldAsync(User user, string field)
        {
            Type type = typeof(User);
            var f = type.GetField(field);
            var value = f.GetValue(user);
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<User>.Update.Set(field, value);
            await _context.Users.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Users.DeleteOneAsync(u => u.Id == id);
        }
    }
}
