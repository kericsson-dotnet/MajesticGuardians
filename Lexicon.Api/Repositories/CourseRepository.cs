using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories
{
    public class CourseRepository : CrudRepository<Course> ,ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {

        }

        public void AddUserToCourse(int courseId, User user)
        {
            //var course = _context.Set<Course>()
            //                .Include(c => c.Users)
            //                .FirstOrDefault(c => c.CourseId == courseId);

            //if (course != null) 
            //{
            //    course.Users.Add(user);
            //    _context.SaveChanges(); 
            //}
        }
    }
}
