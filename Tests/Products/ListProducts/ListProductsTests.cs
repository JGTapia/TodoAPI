using System.Net;

using TodoApi.Endpoints.Products.ListProducts;

namespace TodoApi.Tests.Products.ListProducts;

public class ListProductsTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Should_Return_Default_Products()
    {
        // Act
        var (rsp, res) = await App.Client.GETAsync<ListProductsEndpoint, EmptyRequest, ListProductsResponse>(new());

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        res.Products.ShouldNotBeNull();
        res.Products.Count.ShouldBe(10); // Adjust as needed for your test data
    }

    [Fact]
    public async Task Should_Return_5_Products()
    {
        // Act
        var (rsp, res) = await App.Client.GETAsync<ListProductsEndpoint, ListProductsRequest, ListProductsResponse>(
            new ListProductsRequest { PageSize = 5 });

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
        res.Products.ShouldNotBeNull();
        res.Products.Count.ShouldBe(5); // Adjust as needed for your test data
    }
}