using System.Collections;

namespace Lexicon.Api.Entities;

public class Document
{
    public int DocumentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User AddedBy { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime TimeAdded { get; set; } = DateTime.Now;
    public int? ModuleId { get; set; } 
    public Module Module { get; set; }
    public int? CourseId { get; set; }
    public Course Course { get; set; }
    public int? ActivityId { get; set; }
    public Activity Activity { get; set; }
}
