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

        public async Task<IEnumerable<User>> GetAllUsersInCourse(int courseId)
        {
            var course = await _context.Set<Course>()
                                       .Include(c => c.Users)
                                       .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null)
            {
                throw new InvalidOperationException($"Course with id {courseId} not found");
            }

            return course.Users;
        }

        public async Task<IEnumerable<User>> GetAllAvailableUserForCourse(int courseId)
        {

            var studentIdsInCourses = await _context
                                                .Set<Course>()
                                                .SelectMany(c => c.Users)
                                                .Where(u => u.Role == UserRole.Student)
                                                .Select(u => u.UserId)
                                                .ToListAsync();

            var availableStudents = await _context
                                            .Set<User>()
                                            .Where(u => u.Role == UserRole.Student && !studentIdsInCourses.Contains(u.UserId))
                                            .ToListAsync();


            var course = await _context.Set<Course>()
                                       .Include(c => c.Users)
                                       .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if(course == null)
            {
                throw new InvalidOperationException($"Course with id {courseId} not found");
            }

            var teacherIdsInCourse = await _context
                                            .Set<Course>()
                                            .Where(c => c.CourseId == courseId)
                                            .SelectMany(c => c.Users)
                                            .Where(u => u.Role == UserRole.Teacher)
                                            .Select(u => u.UserId)
                                            .ToListAsync();

            var availableTeachers = await _context                                            .Set<User>()
                                            .Where(u => u.Role == UserRole.Teacher && !teacherIdsInCourse.Contains(u.UserId))
                                            .ToListAsync();

            return availableTeachers.Concat(availableStudents);

        }
    }
}
