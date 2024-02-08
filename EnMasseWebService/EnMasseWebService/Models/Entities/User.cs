using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace EnMasseWebService.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public ICollection<UserContacts> UserContacts { get; set; }
        public ICollection<UserContacts> ContactUsers { get; set; }
        public string? UserPhotoId { get; set; }

    }
}
