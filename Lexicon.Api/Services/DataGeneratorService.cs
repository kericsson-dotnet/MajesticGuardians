﻿using Bogus;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Lexicon.Api.Services;

public class DataGeneratorService(IUnitOfWork unitOfWork)
{
    public async Task GenerateDataAsync()
    {
        if (await unitOfWork.Users.CountAsync() == 0)
        {
            await GenerateUsersAsync();
            await GenerateCoursesAsync();
            await GenerateModulesAsync();
            await GenerateActivitiesAsync();
            await GenerateDocumentsAsync();
            await AddExampleUsersToCoursesAsync();
        }
    }

    private async Task GenerateDocumentsAsync()
    {
        var exampleTeacher = await unitOfWork.Users.GetAsync(1);
        foreach (var module in await unitOfWork.Modules.GetAllAsync())
        {
            for (var i = 0; i < 4; i++)
            {
                var document = new Faker<Document>("sv")
                    .RuleFor(d => d.Name, f => f.Hacker.Noun())
                    .RuleFor(d => d.Description, f => f.Hacker.Phrase())
                    .RuleFor(d => d.AddedBy, _ => exampleTeacher)
                    .RuleFor(d => d.Module, _ => module)
                    .RuleFor(d => d.Url, f => f.Internet.Url())
                    .RuleFor(d => d.TimeAdded, f => f.Date.Past())
                    .Generate();
                unitOfWork.Documents.Add(document);
            }
        }

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateModulesAsync()
    {
        var seedModules = SeedData.GetSeedModules();
        foreach (var course in await unitOfWork.Courses.GetAllAsync())
        {
            for (var i = 0; i < 3; i++)
            {
                var seedModule = seedModules.First();
                seedModules.RemoveAt(0);
                var module = new Faker<Module>("sv")
                    .RuleFor(m => m.Course, _ => course)
                    .RuleFor(m => m.Name, _ => seedModule.Name)
                    .RuleFor(m => m.Description, f => seedModule.Description)
                    .RuleFor(m => m.StartDate, f => f.Date.Past())
                    .RuleFor(m => m.EndDate, f => f.Date.Future())
                    // .RuleFor(m => m.Documents, f => faker.PickRandom(documents, 2).ToList())
                    .Generate();
                unitOfWork.Modules.Add(module);
            }
        }

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateActivitiesAsync()
    {
        var seedActivities = SeedData.GetSeedActivities();
        foreach (var module in await unitOfWork.Modules.GetAllAsync())
        {
            for (var i = 0; i < 3; i++)
            {
                var seedActivity = seedActivities.First();
                seedActivities.RemoveAt(0);
                var activity = new Faker<Activity>("sv")
                    .RuleFor(a => a.Module, _ => module)
                    .RuleFor(a => a.Name, _ => seedActivity.Name)
                    .RuleFor(a => a.Description, _ => seedActivity.Description)
                    .RuleFor(a => a.Type, _ => seedActivity.Type)
                    .RuleFor(a => a.StartDate, f => f.Date.Past())
                    .RuleFor(a => a.EndDate, f => f.Date.Future())
                    // .RuleFor(a => a.Documents, f => faker.PickRandom(documents, 1).ToList())
                    .Generate();
                unitOfWork.Activities.Add(activity);
            }
        }

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateCoursesAsync()
    {
        var faker = new Faker("sv");
        var allUsers = await unitOfWork.Users.GetAllAsync();
        var availableUsers = new List<User>(allUsers);
        var courses = new[]
        {
            new Course
            {
                Name = ".net 2024",
                Description = "Kursen .NET 2024 är en kurs som lär dig grunderna i .NET och C#. Den behandlar bland annat databasehantering, webbutveckling och API:er.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Course
            {
                Name = "Python 2024",
                Description = "Kursen Python 2024 är en kurs som lär dig grunderna i Python. Den behandlar bland annat webbutveckling, maskininlärning och dataanalys.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            }
        };
        foreach (var course in courses)
        {
            var usersToAdd = Math.Min(10, availableUsers.Count);
            var selectedUsers = faker.PickRandom(availableUsers, usersToAdd).ToList();
            
            course.Users = selectedUsers;
            unitOfWork.Courses.Add(course);

            // Remove selected users from the available pool
            availableUsers.RemoveAll(u => selectedUsers.Contains(u));
        }

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateUsersAsync()
    {
        // Create generic teacher and student for testing/demo purposes
        unitOfWork.Users.Add(new User
        {
            FirstName = "Sven",
            LastName = "Lärarsson",
            Email = "teacher@example.com",
            Password = "teacher",
            Role = UserRole.Teacher,
        });
        unitOfWork.Users.Add(new User
        {
            FirstName = "Anders",
            LastName = "Studentsson",
            Email = "student@example.com",
            Password = "student",
            Role = UserRole.Student,
        });

        // Create 2 random teachers
        for (var i = 0; i < 2; i++)
        {
            var user = new Faker<User>("sv")
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Role, _ => UserRole.Teacher)
                .Generate();

            unitOfWork.Users.Add(user);
        }

        // Create 20 random students
        for (var i = 0; i < 20; i++)
        {
            var user = new Faker<User>(locale: "sv")
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Role, _ => UserRole.Student)
                .Generate();

            unitOfWork.Users.Add(user);
        }

        await unitOfWork.SaveAsync();
    }

    private async Task AddExampleUsersToCoursesAsync()
    {
        var exampleTeacher = await unitOfWork.Users.GetAsync(1);
        var exampleStudent = await unitOfWork.Users.GetAsync(2);
        var courses = await unitOfWork.Courses.GetAllAsync();
        foreach (var course in courses)
        {
            if (course.Users.All(u => u.UserId != exampleTeacher.UserId))
            {
                course.Users.Add(exampleTeacher);
            }
        }

        if (exampleStudent.Courses.IsNullOrEmpty())
        {
            unitOfWork.Courses.AddUserToCourse(1, exampleStudent);
        }

        await unitOfWork.SaveAsync();
    }
}