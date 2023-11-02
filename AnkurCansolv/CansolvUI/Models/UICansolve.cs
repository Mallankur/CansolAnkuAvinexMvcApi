using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace CansolvUI.Models
{
    public class UICansolve
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }
        [DisplayName("EVENT TIME")]
        public string EventTime { get; set; }
        public double Value { get; set; }
        [DisplayName("Tag Name")]
        public string TagName { get; set; }
    }
}
