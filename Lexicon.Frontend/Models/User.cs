
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
    [StringLength(100, ErrorMessage = "Förnamn kan inte vara mer än 100 tecken")]
    [MinLength(2, ErrorMessage = "Förnamn måste vara minst 2 tecken")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternamn är obligatoriskt")]
    [StringLength(100, ErrorMessage = "Efternamn kan inte vara mer än 100 tecken")]
    [MinLength(2, ErrorMessage = "Efternamn måste vara minst 2 tecken")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Epost är obligatoriskt")]
    [StringLength(100, ErrorMessage = "Epost kan inte vara mer än 100 tecken")]
    [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lösenord är obligatoriskt")]
    [StringLength(100, ErrorMessage = "Lösenord kan inte vara mer än 100 tecken")]
    [MinLength(4, ErrorMessage = "Lösenord måste vara minst 4 tecken")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Roll är obligatoriskt")]
    public UserRole Role { get; set; } 

    public List<Document> Documents { get; set; } = [];
    public List<int> DocumentIds { get; set; } = [];
    public List<Course> Courses { get; set; } = [];
    public List<int> CourseIds { get; set; } = [];
    public List<UserRole> AllRoles { get { return new List<UserRole>((UserRole[])Enum.GetValues(typeof(UserRole))); } }
}