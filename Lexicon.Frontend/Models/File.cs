using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Lexicon.Frontend.Models
{
    public class File
    {
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Namnet måste vara mellan 4 och 100 tecken")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Beskrivning är obligatorisk")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Beskrivningen måste vara mellan 10 och 500 200")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Att välja en fil är obligatoriskt")]
        public IReadOnlyList<IBrowserFile> Attachments { get; set; }

        public string UserId { get; set; } = string.Empty;
        
        public string? ModuleId { get; set; } = string.Empty;
    }
}
