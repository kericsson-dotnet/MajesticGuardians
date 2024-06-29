using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories
{
    public class CourseRepository : CrudRepository<Course> ,ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {

        }

        public virtual async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Set<Course>()
                                 .Include(c => c.Users) // Include related entities if needed
                                 .ToListAsync();
        }

        public void AddUserToCourse(int courseId, User user)
        {

            if (user.Role == UserRole.Student)
            {

                var isUserInAnyCourse = _context.Set<Course>()
                                            .Include(c => c.Users)
                                            .Any(c => c.Users.Any(u => u.UserId == user.UserId));

                if(isUserInAnyCourse)
                {
                    throw new InvalidOperationException("Student is already register in a course");
                }

            }

            var course = _context.Set<Course>()
                            .Include(c => c.Users)
                            .FirstOrDefault(c => c.CourseId == courseId);

            if (course != null)
            {
                course.Users.Add(user);
            }
        }

        public void RemoveUserFromCourse(int courseId, int userId)
        {
            var course = _context.Set<Course>()
                                 .Include(c => c.Users)
                                 .FirstOrDefault(c => c.CourseId == courseId);

            if(course == null)
            {
                throw new InvalidOperationException($"Course with {courseId} not found");
            }

            var user = course.Users.FirstOrDefault(u => u.UserId == userId);

            if(user == null) 
            {
                throw new InvalidOperationException($"User with id {userId} not found in this course");
            }

            course.Users.Remove(user);
        }
    }
}
