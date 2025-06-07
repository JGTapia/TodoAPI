namespace TodoApi.Endpoints.Products.CreateProduct;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int Price { get; set; }
}