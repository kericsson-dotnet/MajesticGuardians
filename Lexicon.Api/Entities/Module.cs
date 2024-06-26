namespace Lexicon.Api.Entities;

public class Module
{
    public int ModuleId { get; set; }
    public List<Activity> Activities { get; set; } = [];
    public int CourseId { get; set; }
    public virtual Course? Course { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public List<Document> Documents { get; set; } = [];
}
