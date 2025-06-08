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
        if (!db.Products.Any())
        {
            db.Products.AddRange(new[]
            {
                new Product { Name = "Product 1", Description = "Description 1", Manufacturer = "Manufacturer 1", Stock = 5, Price = 100 },
                new Product { Name = "Product 2", Description = "Description 2", Manufacturer = "Manufacturer 2", Stock = 10, Price = 200 },
                new Product { Name = "Product 3", Description = "Description 3", Manufacturer = "Manufacturer 3", Stock = 15, Price = 300 }
            });
            db.SaveChanges();
        }
    }
}
