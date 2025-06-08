using System.Net;

using TodoApi.Endpoints.Products.ListProducts;

namespace TodoApi.Tests.Products.ListProducts;

public class ListProductsTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Should_Return_All_Products()
    {
        // Act
        var (rsp, res) = await App.Client.GETAsync<ListProductsEndpoint, EmptyRequest, ListProductsResponse>(new());

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        res.Products.ShouldNotBeNull();
        res.Products.Count.ShouldBeGreaterThan(0); // Adjust as needed for your test data
    }
}