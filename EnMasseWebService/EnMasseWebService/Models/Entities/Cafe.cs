using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnMasseWebService.Models.Entities
{
    public class Cafe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CafeId { get; set; }
        public string Name { get; set; }
    }
}
