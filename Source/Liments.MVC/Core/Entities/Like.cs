using MongoDB.Bson.Serialization.Attributes;

namespace Liments.MVC.Core.Entities
{
    public class Like
    {
        [BsonElement("author")]
        public string Author { get; set; }
    }
}
