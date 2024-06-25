using Bogus;
using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Data
{
    public class FakeDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LexiconLmsContext(
                serviceProvider.GetRequiredService<DbContextOptions<LexiconLmsContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }
                var faker = new Faker("en");              
                var users = new List<User>();
                for (int i = 0; i < 10; i++)
                {
                    users.Add(new Faker<User>()                 
                        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                        .RuleFor(u => u.LastName, f => f.Name.LastName())
                        .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                        .RuleFor(u => u.Password, f => f.Internet.Password())
                        .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
                        .Generate());
                }
                context.Users.AddRange(users);
                context.SaveChanges();    
                
                var documents = new List<Document>();
                foreach (var user in users)
                {
                    documents.Add(new Faker<Document>()                 
                        .RuleFor(d => d.Name, f => f.Lorem.Sentence())
                        .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
                        .RuleFor(d => d.AddedBy, f => user)
                        .RuleFor(d => d.Url, f => f.Internet.Url())
                        .RuleFor(d => d.TimeAdded, f => f.Date.Past())
                        .Generate());
                }
                context.Documents.AddRange(documents);
                context.SaveChanges();         
                var courses = new List<Course>();
                for (int i = 0; i < 5; i++)
                {
                    var course = new Faker<Course>()
                        .RuleFor(c => c.Name, f => f.Company.CompanyName())
                        .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
                        .RuleFor(c => c.StartDate, f => f.Date.Past())
                        .RuleFor(c => c.EndDate, f => f.Date.Future())
                        .RuleFor(c => c.Users, f => faker.PickRandom(users, 5).ToList())
                        .RuleFor(c => c.Documents, f => faker.PickRandom(documents, 3).ToList())
                        .Generate();
                    courses.Add(course);
                }
                context.Courses.AddRange(courses);
                context.SaveChanges();               
                var modules = new List<Module>();
                courses = context.Courses.ToList();
                foreach (var course in courses)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var module = new Faker<Module>()
                            .RuleFor(m => m.Name, f => f.Lorem.Sentence())
                            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
                            .RuleFor(m => m.StartDate, f => f.Date.Past())
                            .RuleFor(m => m.EndDate, f => f.Date.Future())
                            .RuleFor(m => m.Course, f => f.PickRandom(courses))
                            .RuleFor(m => m.Documents, f => faker.PickRandom(documents, 2).ToList())
                            .Generate();
                        modules.Add(module);
                    }
                }
                context.Modules.AddRange(modules);
                context.SaveChanges();          
                var activities = new List<Activity>();
                foreach (var module in modules)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var activity = new Faker<Activity>()
                            .RuleFor(a => a.Name, f => f.Lorem.Sentence())
                            .RuleFor(a => a.Type, f => f.PickRandom<ActivityType>())
                            .RuleFor(a => a.StartDate, f => f.Date.Past())
                            .RuleFor(a => a.EndDate, f => f.Date.Future())
                            .RuleFor(a => a.Documents, f => faker.PickRandom(documents, 1).ToList())
                            .Generate();
                        activities.Add(activity);
                    }
                }
                context.Activities.AddRange(activities);
                context.SaveChanges();
            }
        }
    }
}
