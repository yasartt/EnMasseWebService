using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EnMasseWebService.Models.Entities
{
    public class SessionMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public Guid CafeId { get; set; }
        public Guid SenderId { get; set; }
        public DateTime SentAt { get; set; }
        public string? Content { get; set; }
    }
}