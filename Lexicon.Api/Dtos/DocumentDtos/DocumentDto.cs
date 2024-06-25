using Lexicon.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Lexicon.Api.Dtos.DocumentDtos
{
    public class DocumentDto
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500, ErrorMessage = "Description length can't be more than 500.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(500, ErrorMessage = "Url length can't be more than 500.")]
        public string Url { get; set; } = string.Empty;

        [Required]
        public DateTime TimeAdded { get; set; } = DateTime.Now;

        public User? AddedBy { get; set; }

    }
}
