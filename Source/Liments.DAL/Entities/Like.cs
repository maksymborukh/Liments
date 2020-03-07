using MongoDB.Bson.Serialization.Attributes;

namespace Liments.DAL.Entities
{
    public class Like
    {
        [BsonElement("author")]
        public string Author { get; set; }
    }
}
