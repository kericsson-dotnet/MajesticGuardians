namespace Lexicon.Api.Entities;

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
    public List<Document>? Documents { get; set; } = [];
    public List<Course>? Courses { get; set; } = new List<Course>();

}


