using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Liments.MVC.Core.Entities
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

        [BsonElement("like")]
        public List<Like> Likes { get; set; }

        [BsonElement("posted_at")]
        public DateTime PostedAt { get; set; }

        [BsonElement("is_posted")]
        public bool IsPosted { get; set; }
    }
}
