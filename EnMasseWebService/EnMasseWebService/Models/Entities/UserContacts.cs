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
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public User User { get; set; }
        public User Contact { get; set; }

    }
}
