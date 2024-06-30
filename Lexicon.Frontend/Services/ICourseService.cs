using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface ICourseService
{
    Task<List<Course>> GetCoursesAsync();
    
    Task<Course> GetCourseAsync(int id);

    Task AddCourseAsync(Course course);

    Task UpdateCourseAsync(Course course);

    Task<List<User>> GetAllUsersInCourse(int id);

    Task DeleteCourseAsync(int id);

    Task RemoveUserFromCourse(int id, int userId);

    Task<List<User>> GetAllAvailableUserForCourse(int id);

    Task AddUserToCourse(int id, int userId);

}
