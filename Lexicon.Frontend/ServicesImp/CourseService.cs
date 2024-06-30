using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using System.Net.Http;

namespace Lexicon.Frontend.ServicesImp;

public class CourseService : ICourseService
{
    private readonly HttpClient _httpClient;

    public CourseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Course>> GetCoursesAsync() => await _httpClient.GetFromJsonAsync<List<Course>>("api/courses");

    public async Task<Course> GetCourseAsync(int id) => await _httpClient.GetFromJsonAsync<Course>($"api/courses/{id}");

    public async Task UpdateCourseAsync(Course course) => await _httpClient.PutAsJsonAsync($"api/courses/{course.CourseId}", course);

    public async Task AddCourseAsync(Course course) => await _httpClient.PostAsJsonAsync($"api/courses", course);

    public async Task<List<User>> GetAllUsersInCourse(int id) => await _httpClient.GetFromJsonAsync<List<User>>($"api/courses/{id}/getAllUsersInCourse");

    public async Task DeleteCourseAsync(int id) => await _httpClient.DeleteAsync($"api/courses/{id}");

    public async Task RemoveUserFromCourse(int id, int userId) => await _httpClient.DeleteAsync($"api/courses/{id}/removeUserFromCourse/{userId}");

    public async Task<List<User>> GetAllAvailableUserForCourse(int id) => await _httpClient.GetFromJsonAsync<List<User>>($"api/courses/{id}/getAllUsersInCourse");
}
