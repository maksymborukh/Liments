using Liments.DAL.Core;
using Liments.DAL.Entities;
using Liments.DAL.Interfaces;

namespace Liments.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private LimentsContext _context;
        private UserRepository _userRepository;
        private PostRepository _postRepository;

        public UnitOfWork(IDbSettings dbSettings)
        {
            _context = new LimentsContext(dbSettings);
        }

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_context);
                }
                return _postRepository;
            }
        }
    }
}
