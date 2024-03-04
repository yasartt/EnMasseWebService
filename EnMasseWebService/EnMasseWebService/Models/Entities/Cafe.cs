using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnMasseWebService.Models.Entities
{
    public class Cafe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid CafeId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
