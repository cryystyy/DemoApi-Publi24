using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.User
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(12)]
        public string Password { get; set; }
    }
}
