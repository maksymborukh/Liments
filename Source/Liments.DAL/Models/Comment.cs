using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Liments.DAL.Models
{
    public class Comment
    {
        [BsonElement("Author")]
        public string Author { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("Like")]
        public int Like { get; set; }

        [BsonElement("PostedAt")]
        public DateTime PostedAt { get; set; }
    }
}
