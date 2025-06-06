// Infrastructure/Data/SeedData.cs
using TodoApi.Models;

namespace TodoApi.Data;

public static class SeedData
{
    public static void Initialize(TodoDbContext db)
    {
        if (!db.Todos.Any())
        {
            db.Todos.AddRange(new[]
            {
                new Todo { Title = "EF Task 1", Done = true },
                new Todo { Title = "EF Task 2", Done = false },
                new Todo { Title = "EF Task 3", Done = false }
            });
            db.SaveChanges();
        }
    }
}
