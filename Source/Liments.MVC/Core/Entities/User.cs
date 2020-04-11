using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Liments.MVC.Core.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("user_name")]
        public string UserName { get; set; }

        [BsonElement("subs")]
        public List<string> Subs { get; set; }

        [BsonElement("fol")]
        public List<string> Fol { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
