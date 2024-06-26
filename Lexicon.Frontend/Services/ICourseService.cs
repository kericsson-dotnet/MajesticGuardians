using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetCoursesAsync();
    }
}
