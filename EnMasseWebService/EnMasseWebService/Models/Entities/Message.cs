using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnMasseWebService.Models.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MessageId { get; set; }

        [Required]
        public string MessageText { get; set; }

        public DateTime DateTime { get; set; }

        public User Sender { get; set; }

    }
}
