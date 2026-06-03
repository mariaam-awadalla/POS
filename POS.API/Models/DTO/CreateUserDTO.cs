namespace POS.API.Models.DTO
{
    using System.ComponentModel.DataAnnotations;

   
        public class CreateUserDTO
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

