using MongoDB.Bson.Serialization.Attributes;

namespace Liments.MVC.Core.Entities
{
    public class Like
    {
        [BsonElement("user_name")]
        public string UserName { get; set; }
    }
}
