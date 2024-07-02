
using System.ComponentModel.DataAnnotations;

namespace Lexicon.Frontend.Models;

public enum UserRole
{
    Teacher,
    Student
}

public class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Förnamn är obligatoriskt")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Förnamn måste vara mellan 4 och 100 tecken")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternamn är obligatoriskt")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Efternamn måste vara mellan 4 och 100 tecken")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Epost är obligatoriskt")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Epost måste vara mellan 4 och 100 tecken")]
    [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lösenord är obligatoriskt")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Lösenord måste vara mellan 4 och 100 tecken")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Roll är obligatoriskt")]
    public UserRole Role { get; set; } 

    public List<Document> Documents { get; set; } = [];
    public List<int> DocumentIds { get; set; } = [];
    public List<Course> Courses { get; set; } = [];
    public List<int> CourseIds { get; set; } = [];
    public List<UserRole> AllRoles { get { return new List<UserRole>((UserRole[])Enum.GetValues(typeof(UserRole))); } }
}