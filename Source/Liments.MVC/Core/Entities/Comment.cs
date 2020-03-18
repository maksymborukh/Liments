using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Liments.MVC.Core.Entities
{
    public class Comment
    {
        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("like")]
        public List<Like> Likes { get; set; }

        [BsonElement("posted_at")]
        public string PostedAt { get; set; }
    }
}
