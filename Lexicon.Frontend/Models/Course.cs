using System.ComponentModel.DataAnnotations;

namespace Lexicon.Frontend.Models;

public class Course
{
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Namn är obligatoriskt")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Namnet måste vara mellan 4 och 100 tecken")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beskrivning är obligatorisk")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Beskrivningen måste vara mellan 10 och 500 tecken")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start datum är obligatoriskt")]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Slut datums är obligatoriskt")]
    public DateTime EndDate { get; set; }

    public List<User> Users { get; set; } = [];
    public List<int> UserIds { get; set; } = [];
    public List<int> DocumentIds { get; set; } = [];
    public List<int> ModuleIds { get; set; } = [];
}