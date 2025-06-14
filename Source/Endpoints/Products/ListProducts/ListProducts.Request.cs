namespace TodoApi.Endpoints.Products.ListProducts;

public class ListProductsRequest
{
    public int? PageSize { get; set; } // Number of items per page
    public int? Page { get; set; }     // Current page number
}