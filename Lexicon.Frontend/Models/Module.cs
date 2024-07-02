using System.ComponentModel.DataAnnotations;

namespace Lexicon.Frontend.Models;

public class Module
{
    public int ModuleId { get; set; }

	[Required(ErrorMessage = "Namn är obligatoriskt")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Namnet måste vara mellan 4 och 100 tecken")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Beskrivning är obligatoriskt")]
	[StringLength(500, MinimumLength = 10, ErrorMessage = "Beskrivningen måste vara mellan 10 och 500 tecken")]
	public string Description { get; set; } = string.Empty;

	[Required(ErrorMessage = "Start datum är obligatoriskt")]
	public DateTime StartDate { get; set; } = DateTime.Now;
	
    [Required(ErrorMessage = "Slut datum är obligatoriskt")]
	public DateTime EndDate { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }
    public List<Activity> Activities { get; set; } = [];
    public List<int> ActivityIds { get; set; } = [];
    public List<Document> Documents { get; set; } = [];
    public List<int> DocumentIds { get; set; } = [];
}
