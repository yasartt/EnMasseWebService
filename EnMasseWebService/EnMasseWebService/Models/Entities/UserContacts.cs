using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnMasseWebService.Models.Entities
{
    public class UserContacts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserContactId { get; set; }
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
