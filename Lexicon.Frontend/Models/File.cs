using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Lexicon.Frontend.Models
{
    public class File
    {
        [Required(ErrorMessage = "Namn är obligatoriskt.")]
        [StringLength(100, ErrorMessage = "Namnet får inte vara längre än 100 tecken.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Beskrivning är obligatorisk.")]
        [StringLength(500, ErrorMessage = "Beskrivningen får inte vara längre än 500 tecken.")]
        public string Description { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        
        public string? ModuleId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Att välja en fil är obligatoriskt")]
        public IReadOnlyList<IBrowserFile> Attachments { get; set; }
    }
}
