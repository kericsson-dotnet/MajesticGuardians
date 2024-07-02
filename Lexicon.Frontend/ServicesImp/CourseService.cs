using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using System.Net;
using System.Net.Http.Headers;

namespace Lexicon.Frontend.ServicesImp;

public class CourseService : ICourseService
{
    private readonly HttpClient _httpClient;
    private readonly ISessionStorageService _sessionStorageService;

    public CourseService(HttpClient httpClient, ISessionStorageService sessionStorageService)
    {
        _httpClient = httpClient;
        _sessionStorageService = sessionStorageService;
    }

    private async Task AddTokenToRequestHeader()
    {
        var token = await _sessionStorageService.GetItemAsync("authToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        await AddTokenToRequestHeader();
        return await _httpClient.GetFromJsonAsync<List<Course>>("api/courses");
    }

    public async Task<Course> GetCourseAsync(int id)
    {
        await AddTokenToRequestHeader();

        try
        {
            return await _httpClient.GetFromJsonAsync<Course>($"api/courses/{id}");
        }

        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task UpdateCourseAsync(Course course) => await _httpClient.PutAsJsonAsync($"api/courses/{course.CourseId}", course);

    public async Task AddCourseAsync(Course course)
    {
        await AddTokenToRequestHeader();
        await _httpClient.PostAsJsonAsync($"api/courses", course);
    }

    public async Task<List<User>> GetAllUsersInCourse(int id)
    {
        await AddTokenToRequestHeader();
        return await _httpClient.GetFromJsonAsync<List<User>>($"api/courses/{id}/getAllUsersInCourse");
    }

    public async Task DeleteCourseAsync(int id)
    {
        await AddTokenToRequestHeader();
        await _httpClient.DeleteAsync($"api/courses/{id}");
    }

    public async Task RemoveUserFromCourse(int id, int userId)
    {
        await AddTokenToRequestHeader();
        await _httpClient.DeleteAsync($"api/courses/{id}/removeUserFromCourse/{userId}");
    }

    public async Task<List<User>> GetAllAvailableUserForCourse(int id)
    {
        await AddTokenToRequestHeader();
        return await _httpClient.GetFromJsonAsync<List<User>>($"api/courses/{id}/getAllAvailableUserForCourse");
    }

    public async Task AddUserToCourse(int id, int userId)
    {
        await AddTokenToRequestHeader();

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

    public async Task<List<Course>> GetAllUsersCourses(int userId)
    {
        await AddTokenToRequestHeader();
        return await _httpClient.GetFromJsonAsync<List<Course>>($"api/courses/getAllUsersCourses/{userId}");
    }
}
