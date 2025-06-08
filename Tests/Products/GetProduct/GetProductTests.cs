using System.Net;

using TodoApi.Endpoints.Products.GetProduct;

namespace TodoApi.Tests.Products.GetProduct;

public class GetProductTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Should_Return_Product_When_Exists()
    {
        // Arrange
        var existingProductId = 2; // Use an existing product ID in your test DB

        // Act
        var (rsp, res) = await App.Client.GETAsync<GetProductEndpoint, GetProductRequest, GetProductResponse>(
            new GetProductRequest { Id = existingProductId });

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        res.Id.ShouldBe(existingProductId);
        res.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Product_Does_Not_Exist()
    {
        // Arrange
        var nonExistentProductId = 9999; // Use a non-existent product ID

        // Act
        var rsp = await App.Client.GETAsync<GetProductEndpoint, GetProductRequest>(
            new GetProductRequest { Id = nonExistentProductId });

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}