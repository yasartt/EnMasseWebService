using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EnMasseWebService.Models.Entities
{
    public class CafeUserLastCollectedMessages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? LastCollectedMessageId { get; set; }

        [ForeignKey("Cafe")]
        public Guid CafeId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Cafe? cafe { get; set; }
    }
}
