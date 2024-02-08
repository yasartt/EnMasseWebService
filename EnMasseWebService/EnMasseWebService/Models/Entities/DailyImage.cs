using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnMasseWebService.Models.Entities
{
    public class DailyImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DailyImageId { get; set; }

        [ForeignKey("Daily")]
        public int DailyId { get; set; }

        public string? Id { get; set; }

        public Daily? Daily { get; set; }

    }
}