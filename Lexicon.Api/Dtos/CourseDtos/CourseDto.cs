using Lexicon.Api.Entities;

namespace Lexicon.Api.Dtos.CourseDtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<User> Users { get; set; } = [];

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }

        public List<Document> Documents { get; set; } = [];
    }
}
