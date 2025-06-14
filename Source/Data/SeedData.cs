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
                new Product { Name = "Product 3", Description = "Description 3", Manufacturer = "Manufacturer 3", Stock = 15, Price = 300 },
                new Product { Name = "Product 4", Description = "Description 4", Manufacturer = "Manufacturer 4", Stock = 20, Price = 400 },
                new Product { Name = "Product 5", Description = "Description 5", Manufacturer = "Manufacturer 5", Stock = 25, Price = 500 },
                new Product { Name = "Product 6", Description = "Description 6", Manufacturer = "Manufacturer 6", Stock = 30, Price = 600 },
                new Product { Name = "Product 7", Description = "Description 7", Manufacturer = "Manufacturer 7", Stock = 35, Price = 700 },
                new Product { Name = "Product 8", Description = "Description 8", Manufacturer = "Manufacturer 8", Stock = 40, Price = 800 },
                new Product { Name = "Product 9", Description = "Description 9", Manufacturer = "Manufacturer 9", Stock = 45, Price = 900 },
                new Product { Name = "Product 10", Description = "Description 10", Manufacturer = "Manufacturer 10", Stock = 50, Price = 1000 },
                new Product { Name = "Product 11", Description = "Description 11", Manufacturer = "Manufacturer 11", Stock = 55, Price = 1100 },
                new Product { Name = "Product 12", Description = "Description 12", Manufacturer = "Manufacturer 12", Stock = 60, Price = 1200 },
                new Product { Name = "Product 13", Description = "Description 13", Manufacturer = "Manufacturer 13", Stock = 65, Price = 1300 },
                new Product { Name = "Product 14", Description = "Description 14", Manufacturer = "Manufacturer 14", Stock = 70, Price = 1400 },
                new Product { Name = "Product 15", Description = "Description 15", Manufacturer = "Manufacturer 15", Stock = 75, Price = 1500 },
                new Product { Name = "Product 16", Description = "Description 16", Manufacturer = "Manufacturer 16", Stock = 80, Price = 1600 },
                new Product { Name = "Product 17", Description = "Description 17", Manufacturer = "Manufacturer 17", Stock = 85, Price = 1700 },
                new Product { Name = "Product 18", Description = "Description 18", Manufacturer = "Manufacturer 18", Stock = 90, Price = 1800 },
                new Product { Name = "Product 19", Description = "Description 19", Manufacturer = "Manufacturer 19", Stock = 95, Price = 1900 },
                new Product { Name = "Product 20", Description = "Description 20", Manufacturer = "Manufacturer 20", Stock = 100, Price = 2000 },
                new Product { Name = "Product 21", Description = "Description 21", Manufacturer = "Manufacturer 21", Stock = 105, Price = 2100 },
                new Product { Name = "Product 22", Description = "Description 22", Manufacturer = "Manufacturer 22", Stock = 110, Price = 2200 },
                new Product { Name = "Product 23", Description = "Description 23", Manufacturer = "Manufacturer 23", Stock = 115, Price = 2300 },
                new Product { Name = "Product 24", Description = "Description 24", Manufacturer = "Manufacturer 24", Stock = 120, Price = 2400 },
            });
            db.SaveChanges();
        }
    }
}
