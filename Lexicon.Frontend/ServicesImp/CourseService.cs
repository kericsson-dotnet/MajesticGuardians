using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;

namespace Lexicon.Frontend.ServicesImp
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>("api/courses");
        }

        public async Task<Course> GetStudentCourseAsync(int studentId)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"api/courses/student/{studentId}");
        }

    }
}