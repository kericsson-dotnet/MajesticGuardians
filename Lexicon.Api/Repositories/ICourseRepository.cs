using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories
{
    public interface ICourseRepository : ICrudRepository<Course>
    {
        void AddUserToCourse(int courseId, User user);

        void RemoveUserFromCourse(int courseId, int userId);

        Task<IEnumerable<User>> GetAllUsersInCourse(int courseId);

        Task<IEnumerable<User>> GetAllAvailableUserForCourse(int courseId);

    }
}
