using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EnMasseWebService.Models.DTOs
{
    public class ImageDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string ImageName { get; set; } = null!;

        public int DailyId { get; set; }

    }
}
