using Lexicon.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Lexicon.Api.Dtos.UserDtos
{
    public class UserDto
    {

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "First Name length can't be more than 100.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Last Name length can't be more than 100.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(200, ErrorMessage = "Email length can't be more than 200.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Password length can't be more than 100.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } = UserRole.Student;

        //public List<int> DocumentIds { get; set; } = [];

        //public List<int> Courseds { get; set; } = [];

    }
}
