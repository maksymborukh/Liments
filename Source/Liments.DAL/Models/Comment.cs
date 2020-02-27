using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Liments.DAL.Models
{
    public class Comment
    {
        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("like")]
        public int Like { get; set; }

        [BsonElement("posted_at")]
        public DateTime PostedAt { get; set; }
    }
}
