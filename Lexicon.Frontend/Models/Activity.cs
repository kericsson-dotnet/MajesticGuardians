using System.ComponentModel.DataAnnotations;

namespace Lexicon.Frontend.Models;

public enum ActivityType
{
    Lecture,
    ELearning,
    Assignment,
    Quiz,
    GroupAssignment,
}

public class Activity
{
    public int ActivityId { get; set; }

	[Required(ErrorMessage = "Namn är obligatoriskt")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Namnet måste vara mellan 4 och 100 tecken")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Beskrivning är obligatorisk")]
	[StringLength(500, MinimumLength = 10, ErrorMessage = "Beskrivningen måste vara mellan 10 och 500 tecken")]
	public string Description { get; set; } = string.Empty;

	[Required(ErrorMessage = "Start datum är obligatoriskt")]
	public DateTime StartDate { get; set; } = DateTime.Now;

	[Required(ErrorMessage = "Slut datum är obligatoriskt")]
	public DateTime EndDate { get; set; }

	[Required(ErrorMessage = "Aktivitet är obligatoriskt")]
	public ActivityType Type { get; set; }

	public Module Module { get; set; }
	public int ModuleId { get; set; }
	public List<Document> Documents { get; set; } = [];

    public List<ActivityType> AllActivities { get { return new List<ActivityType>((ActivityType[])Enum.GetValues(typeof(ActivityType))); } }

}