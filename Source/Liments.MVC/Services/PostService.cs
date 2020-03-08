using Liments.MVC.Core.Database;
using Liments.MVC.Core.Entities;
using Liments.MVC.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liments.MVC.Services
{
    public class PostService : IPostService
    {
        private ILimentsContext _context;

        public PostService(ILimentsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var result = await _context.Posts.AsQueryable().ToListAsync();
            return result;
        }

        public async Task<Post> GetByAuthorAsync(string str)
        {
            var result = await _context.Posts.Find(p => p.Author == str).SingleOrDefaultAsync();
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

        public async Task DeleteAsync(string id)
        {
            await _context.Posts.DeleteOneAsync(p => p.Id == id);
        }
    }
}
