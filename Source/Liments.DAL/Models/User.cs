using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Liments.DAL.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("AccSubs")]
        public List<string> AccSubs { get; set; }

        [BsonElement("PndSubs")]
        public List<string> PndSubs { get; set; }

        [BsonElement("AccFol")]
        public List<string> AccFol { get; set; }

        [BsonElement("PndFol")]
        public List<string> PndFol { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
