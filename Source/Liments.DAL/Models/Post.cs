using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Liments.DAL.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Author")]
        public string Author { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("Tags")]
        public List<string> Tags { get; set; }

        [BsonElement("Comments")]
        public List<Comment> Comments { get; set; }

        [BsonElement("Like")]
        public int Like { get; set; }

        [BsonElement("View")]
        public int View { get; set; }

        [BsonElement("PostedAt")]
        public DateTime PostedAt { get; set; }

        [BsonElement("IsPosted")]
        public bool IsPosted { get; set; }

    }
}
