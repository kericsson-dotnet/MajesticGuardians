using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories;

public class CourseRepository : CrudRepository<Course>, ICourseRepository
{
    public CourseRepository(DbContext context) : base(context)
    {

    }

    public virtual async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context
            .Set<Course>()
            .Include(c => c.Users)
            .Include(c => c.Modules)
            .ToListAsync();
    }

    public virtual async Task<Course> GetAsync(object id)
    {
        if (id is int courseId)
        {
            return await _context
                .Set<Course>()
                .Include(c => c.Users)
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.CourseId == courseId) ?? throw new InvalidOperationException($"{typeof(Course).Name} Id {id} not found.");
        }

        throw new ArgumentException("Invalid ID type");
    }

    public void AddUserToCourse(int courseId, User user)
    {
        try
        {
            if (user.Role == UserRole.Student)
            {
                var isUserInAnyCourse = _context
                    .Set<Course>()
                    .Include(c => c.Users)
                    .Any(c => c.Users.Any(u => u.UserId == user.UserId));

                if (isUserInAnyCourse)
                {
                    throw new InvalidOperationException("Student is already registered in a course.");
                }
            }

            var course = _context
                .Set<Course>()
                .Include(c => c.Users)
                .FirstOrDefault(c => c.CourseId == courseId);

            if (course == null)
            {
                throw new InvalidOperationException($"Course with id {courseId} not found.");
            }

            if (!course.Users.Any(u => u.UserId == user.UserId))
            {
                course.Users.Add(user);
            }

            else
            {
                throw new InvalidOperationException($"User with id {user.UserId} is already registered in course {courseId}.");
            }
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Operation failed: {ex.Message}");
        }

        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}");
        }
    }
    
    public void RemoveUserFromCourse(int courseId, int userId)
    {
        var course = _context
            .Set<Course>()
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
        var course = await _context
            .Set<Course>()
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

        var availableTeachers = await _context     
            .Set<User>()
            .Where(u => u.Role == UserRole.Teacher && !teacherIdsInCourse.Contains(u.UserId))
            .ToListAsync();

        return availableTeachers.Concat(availableStudents);

    }

    public async Task<IEnumerable<Course>> GetAllUserCourses(int userId)
    {
        return await _context
                .Set<Course>()
                .Include(c => c.Users)
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .ToListAsync();
    }
}