using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnMasseWebService.Models.Entities
{
    public class Daily
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DailyId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("DailyType")]
        public int DailyTypeId { get; set; }

        public int PhotoNumber { get; set; }

        public int VideoNumber { get; set; }

        public string? Caption { get; set; }

        public DailyType DailyType { get; set; }

        public User User { get; set; }

    }
}