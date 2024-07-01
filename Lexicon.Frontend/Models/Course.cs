namespace Lexicon.Frontend.Models;

public class Course
{
    public int CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<User> Users { get; set; } = [];
    public List<int> UserIds { get; set; } = [];
    public List<int> DocumentIds { get; set; } = [];
    public List<int> ModuleIds { get; set; } = [];
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
}