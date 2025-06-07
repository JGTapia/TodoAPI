namespace TodoApi.Endpoints.Products.GetProduct;

public class GetProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int Price { get; set; }
}