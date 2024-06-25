using Lexicon.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Lexicon.Api.Dtos.ActivityDtos
{
    public class ActivityPostDto
    {
        [Required]
        public ActivityType Type { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDate { get; set; }
    }
}
