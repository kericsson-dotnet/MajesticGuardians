namespace Lexicon.Api.Entities;

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
    public int ModuleId { get; set; }
    public Module? Module { get; set; }
    public ActivityType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public List<Document> Documents { get; set; } = [];
}
