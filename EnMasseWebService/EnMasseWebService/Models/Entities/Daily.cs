using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnMasseWebService.Models.Entities
{
    public class Daily
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid DailyId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string? Caption { get; set; }

        public User User { get; set; }

        public DateTime? Created { get; set; }

    }
}