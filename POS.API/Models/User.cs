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

            public int Age { get; set; }

            public string Gender { get; set; }

            [Required]
            public string Email { get; set; }

        
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    }

