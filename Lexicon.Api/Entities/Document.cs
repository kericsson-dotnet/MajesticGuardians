namespace Lexicon.Api.Entities;

public class Document
{
    public int DocumentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public User? AddedBy { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime TimeAdded { get; set; } = DateTime.Now;
}
