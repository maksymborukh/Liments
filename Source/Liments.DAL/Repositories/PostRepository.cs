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
    public class PostRepository : IRepository<Post>
    {
        private LimentsContext _context;

        public PostRepository(LimentsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var result = await _context.Posts.AsQueryable().ToListAsync();
            return result;
        }

        public async Task<Post> GetAsync(string str)
        {
            var result = await _context.Posts.Find(p => p.Author == str).SingleOrDefaultAsync();
            return result;
        }

        public IEnumerable<Post> Find(Func<Post, bool> predicate)
        {
            var result = _context.Posts.AsQueryable().Where(predicate).ToList();
            return result;
        }

        public async Task CreateAsync(Post post)
        {
            await _context.Posts.InsertOneAsync(post);
        }

        public async Task UpdateAsync(Post post)
        {
            await _context.Posts.ReplaceOneAsync(p => p.Id == post.Id, post);
        }

        public async Task UpdateFieldAsync(Post post, string field)
        {
            Type type = typeof(Post);
            var f = type.GetField(field);
            var value = f.GetValue(post);
            var filter = Builders<Post>.Filter.Eq(p => p.Id, post.Id);
            var update = Builders<Post>.Update.Set(field, value);
            await _context.Posts.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Posts.DeleteOneAsync(p => p.Id == id);
        }
    }
}
