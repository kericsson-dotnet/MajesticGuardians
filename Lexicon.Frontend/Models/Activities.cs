using Lexicon.Api.Entities;

namespace Lexicon.Frontend.Models
{
    public enum ActivityType
    {
        Lecture,
        Assignment
    }
    public class Activities
    {
        public ActivityType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
    }
}
