using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnMasseWebService.Models.Entities
{
    public class CafeMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CafeMessageId { get; set; }

        [Required]
        public string CafeMessageText { get; set; }

        public DateTime DateTime { get; set; }

        [ForeignKey("User")]
        public int SenderUserId { get; set; }

        [ForeignKey("Cafe")]
        public int CafeId { get; set; }

        public User User { get; set; }
        public Cafe Cafe { get; set; }

    }
}
