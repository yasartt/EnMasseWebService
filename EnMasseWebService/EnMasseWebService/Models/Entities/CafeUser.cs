using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnMasseWebService.Models.Entities
{
    public class CafeUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CafeUserId { get; set; }

        [ForeignKey("Cafe")]
        public int CafeId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public Cafe cafe { get; set; }
    }
}
