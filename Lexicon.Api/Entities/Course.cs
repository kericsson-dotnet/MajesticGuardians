namespace Lexicon.Api.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public List<User> Users { get; set; } = [];
    public List<Document> Documents { get; set; } = [];
    public List<Module> Modules { get; set; } = [];
}

