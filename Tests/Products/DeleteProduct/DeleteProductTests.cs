using System.Net;

using FastEndpoints;

using TodoApi.Endpoints.Products.DeleteProduct;

namespace TodoApi.Tests.Products.DeleteProduct;

public class DeleteProductTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Should_Delete_Product_Successfully()
    {
        // Arrange
        var productId = 1; // Use an existing product ID in your test DB

        // Act
        var rsp = await App.Client.DELETEAsync<DeleteProductEndpoint, DeleteProductRequest>(new DeleteProductRequest { Id = productId });

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Return_NotFound_For_Nonexistent_Product()
    {
        // Arrange
        var productId = 9999; // Use a non-existent product ID

        // Act
        var rsp = await App.Client.DELETEAsync<DeleteProductEndpoint, DeleteProductRequest>(new DeleteProductRequest { Id = productId });

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}