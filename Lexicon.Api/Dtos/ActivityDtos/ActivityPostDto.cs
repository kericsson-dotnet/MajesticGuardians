using Lexicon.Api.Entities;

namespace Lexicon.Api.Dtos.ActivityDtos
{
    public class ActivityPostDto
    {
        public ActivityType Type { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }
    }
}
