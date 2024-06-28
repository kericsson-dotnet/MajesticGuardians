using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories
{
    public interface ICourseRepository : ICrudRepository<Course>
    {
        void AddUserToCourse(int courseId, User user);
    }
}
