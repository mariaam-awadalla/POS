using System.ComponentModel.DataAnnotations;

namespace POS.API.Models.DTO
{
    public class UpdateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
