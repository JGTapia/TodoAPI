
namespace TodoApi.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Manufacturer { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}