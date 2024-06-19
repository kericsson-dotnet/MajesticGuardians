namespace Lexicon.Api.Entities;

public enum ActivityType
{
    Lecture,
    Assignment
}

public class Activity
{
    public int ActivityId { get; set; }
    public ActivityType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public List<Document> Documents { get; set; } = [];
}
