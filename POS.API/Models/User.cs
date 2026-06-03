using System.ComponentModel.DataAnnotations;

namespace POS.API.Models
{

        public class User
        {
            [Key]
            public int Id { get; set; }

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


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    }

