using TodoApi.Endpoints.Products.CreateProduct;

namespace TodoApi.Tests.CreateProduct;

public class CreateProductTests(App App) : TestBase<App>
{
    [Fact]
    public async Task Should_Create_Product_Successfully()
    {

        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Test",
            Description = "Test Desc",
            Manufacturer = "Test Inc",
            Stock = 10,
            Price = 100
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, CreateProductResponse>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);
        res.Id.ShouldNotBe(0);
        res.Id.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task Should_Error_NoName()
    {

        // Arrange
        var request = new CreateProductRequest
        {
            Description = "Test Desc",
            Manufacturer = "Test Manufacturer",
            Stock = 10,
            Price = 100
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.Count().ShouldBe(1);
        res.Errors.Select(e => e.Name).ShouldBe(["name"]);
    }

    [Fact]
    public async Task Should_Error_NoDescription()
    {

        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Test Name",
            Manufacturer = "Test Manufacturer",
            Stock = 10,
            Price = 100
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.Count().ShouldBe(1);
        res.Errors.Select(e => e.Name).ShouldBe(["description"]);
    }

    [Fact]
    public async Task Should_Error_NoNameNoDescription()
    {

        // Arrange
        var request = new CreateProductRequest
        {
            Manufacturer = "Test Manufacturer",
            Stock = 10,
            Price = 100
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errKeys = res.Errors.Select(e => e.Name).ToList();
        errKeys.ShouldBe(
        [
            "name",
            "description"
        ]);
    }

    [Fact]
    public async Task Should_Error_AmountZero()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Test",
            Description = "Test Desc",
            Manufacturer = "Test Manufacturer",
            Stock = 10,
            Price = 0
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.Count().ShouldBe(1);
        res.Errors.Select(e => e.Name).ShouldBe(["price"]);
    }

    [Fact]
    public async Task Should_Error_StockZero()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Test",
            Description = "Test Desc",
            Manufacturer = "Test Manufacturer",
            Stock = 0,
            Price = 100
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.Count().ShouldBe(1);
        res.Errors.Select(e => e.Name).ShouldBe(["stock"]);
    }

    [Fact]
    public async Task Should_Error_AmountZeroStockZero()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Test",
            Description = "Test Desc",
            Manufacturer = "Test Manufacturer",
            Stock = 0,
            Price = 0
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errKeys = res.Errors.Select(e => e.Name).ToList();
        errKeys.ShouldBe(
        [
            "stock",
            "price"
        ]);
    }

    [Fact]
    public async Task Should_Error_NoManufacturer()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Test",
            Description = "Test Desc",
            Stock = 10,
            Price = 100
        };

        // Act
        var (rsp, res) = await App.Client.POSTAsync<CreateProductEndpoint, CreateProductRequest, ProblemDetails>(request);

        // Assert
        rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        res.Errors.Count().ShouldBe(1);
        res.Errors.Select(e => e.Name).ShouldBe(["manufacturer"]);
    }
}