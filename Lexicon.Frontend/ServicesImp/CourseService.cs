using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using System.Net;

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

    public async Task<List<User>> GetAllAvailableUserForCourse(int id) => await _httpClient.GetFromJsonAsync<List<User>>($"api/courses/{id}/getAllAvailableUserForCourse");

    public async Task AddUserToCourse(int id, int userId)
    {
        string url = $"api/courses/{id}/addUserToCourse/{userId}";

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var successMessage = await response.Content.ReadAsStringAsync();
            }

            else if (response.StatusCode == HttpStatusCode.NotFound)
            { 
                throw new Exception("Course or user not found.");
            }

            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Invalid request parameters.");
            }
            
            else
            {
                throw new Exception($"POST request failed with status code {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}"); 
        }
    }

    public async Task<List<Course>> GetAllUsersCourses(int userId) => await _httpClient.GetFromJsonAsync<List<Course>>($"api/courses/getAllUsersCourses/{userId}");
}
