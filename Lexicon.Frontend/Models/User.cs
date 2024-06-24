using Lexicon.Api.Entities;

namespace Lexicon.Frontend.Models
{
    public enum UserRole
    {
        Teacher,
        Student
    }
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Student;

    }
}
