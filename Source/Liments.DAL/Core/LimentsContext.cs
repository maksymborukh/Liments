using Liments.DAL.Entities;
using MongoDB.Driver;

namespace Liments.DAL.Core
{
    public class LimentsContext : ILimentsContext
    {
        private IMongoCollection<User> _users;
        private IMongoCollection<Post> _posts;
        private IMongoDatabase _mongodb;

        public LimentsContext(IDbSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);

            if (client != null)
                _mongodb = client.GetDatabase(dbSettings.DatabaseName);
        }

        public IMongoCollection<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = _mongodb.GetCollection<User>("user");
                }
                return _users;
            }
        }

        public IMongoCollection<Post> Posts
        {
            get
            {
                if (_posts == null)
                {
                    _posts = _mongodb.GetCollection<Post>("post");
                }
                return _posts;
            }
        }
    }
}
