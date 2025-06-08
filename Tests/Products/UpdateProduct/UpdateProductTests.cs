using System.Net;

using TodoApi.Endpoints.Products.UpdateProduct;

namespace TodoApi.Tests.Products.UpdateProduct;

public class UpdateProductTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Should_Update_Product_Successfully()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 2, // Use an existing product ID in your test DB
            Name = "Updated Name",
            Description = "Updated Description",
            Manufacturer = "Updated Manufacturer",
            Stock = 20,
            Price = 200
        };

        // Act
        var rsp = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Update_Product_Name_Successfully()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 2, // Use an existing product ID in your test DB
            Name = "Updated Only Name"
        };

        // Act
        var rsp = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Update_Product_Description_Successfully()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 2, // Use an existing product ID in your test DB
            Description = "Updated Only Description"
        };

        // Act
        var rsp = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Update_Product_Manufacturer_Successfully()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 2, // Use an existing product ID in your test DB
            Manufacturer = "Updated Only Manufacturer"
        };

        // Act
        var rsp = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Update_Product_Stock_Successfully()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 2, // Use an existing product ID in your test DB
            Stock = 30
        };

        // Act
        var rsp = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Update_Product_Price_Successfully()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 2, // Use an existing product ID in your test DB
            Price = 50
        };

        // Act
        var rsp = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Product_Does_Not_Exist()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 9999, // Non-existent product ID
            Name = "Name",
            Description = "Desc",
            Manufacturer = "Manu",
            Stock = 10,
            Price = 100
        };

        // Act
        var rsp = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_Data_Invalid()
    {
        // Arrange
        var request = new UpdateProductRequest
        {
            Id = 2
        };

        // Act
        var (rsp, res) = await App.Client.PUTAsync<UpdateProductEndpoint, UpdateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.ShouldNotBeEmpty();
    }
}