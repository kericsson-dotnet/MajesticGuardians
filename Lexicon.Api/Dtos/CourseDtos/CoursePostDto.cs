using Lexicon.Api.Entities;

namespace Lexicon.Api.Dtos.CourseDtos
{
    public class CoursePostDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }
    }
}
