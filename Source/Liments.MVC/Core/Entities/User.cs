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

        [BsonElement("login")]
        public string Login { get; set; }

        [BsonElement("acc_subs")]
        public List<string> AccSubs { get; set; }

        [BsonElement("pnd_subs")]
        public List<string> PndSubs { get; set; }

        [BsonElement("acc_fol")]
        public List<string> AccFol { get; set; }

        [BsonElement("pnd_fol")]
        public List<string> PndFol { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
