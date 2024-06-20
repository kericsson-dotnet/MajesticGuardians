using Lexicon.Api.Entities;

namespace Lexicon.Api.Dtos.UserDtos
{
    public class UserPostDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Student;
    }
}
