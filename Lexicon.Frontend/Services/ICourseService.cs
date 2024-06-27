﻿using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface ICourseService
{
    Task<List<Course>> GetCoursesAsync();
    Task<Course> GetCourseAsync(int id);

    Task UpdateCourseAsync(Course course);
}
