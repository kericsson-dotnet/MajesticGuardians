using Lexicon.Api.Entities;

namespace Lexicon.Api.Dtos
{
    public class DocumentPostDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public DateTime TimeAdded { get; set; } = DateTime.Now;
    }
}
