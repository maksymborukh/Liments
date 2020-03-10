using MongoDB.Bson.Serialization.Attributes;

namespace Liments.MVC.Core.Entities
{
    public class Access
    {
        [BsonElement("public")]
        public bool Public { get; set; }

        [BsonElement("private")]
        public bool Private { get; set; }

        [BsonElement("friend")]
        public bool Friend { get; set; }
    }
}
