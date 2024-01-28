using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EnMasseWebService.Models.DTOs
{
    public class ImageIncomingDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string ImageName { get; set; } = null!;
    }
}