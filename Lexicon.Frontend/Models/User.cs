
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

        public UserRole Role { get; set; } 
        public List<Document>? Documents { get; set; } = [];
        public List<UserRole> AllRoles { get { return new List<UserRole>((UserRole[])Enum.GetValues(typeof(UserRole))); } }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
