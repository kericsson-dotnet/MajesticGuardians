using Lexicon.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Lexicon.Api.Dtos.CourseDtos;

public class CourseDto
{
    [Key]
    public int CourseId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(500, ErrorMessage = "Description length can't be more than 500.")]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [Required]
    public DateTime EndDate { get; set; }

    public List<int> UserIds { get; set; } = [];

    public List<int> DocumentIds { get; set; } = [];

    public List<int> ModuleIds { get; set; } = [];
}