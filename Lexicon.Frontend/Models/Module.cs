namespace Lexicon.Frontend.Models;

public class Module
{
    public int ModuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public Course? Course { get; set; }
    public List<Activity> Activities { get; set; } = [];
    public List<int> ActivityIds { get; set; } = [];
    public List<Document> Documents { get; set; } = [];
    public List<int> DocumentIds { get; set; } = [];
}
