using Lexicon.Api.Entities;

namespace Lexicon.Api.Dtos.ModuleDtos
{
    public class ModuleDto
    {
        public int ModuleId { get; set; }

        public List<Activity> Activities { get; set; } = [];
       
        public Course? Course { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public DateTime StartDate { get; set; } = DateTime.Now;
        
        public DateTime EndDate { get; set; }
        
        public List<Document> Documents { get; set; } = [];
    }
}
