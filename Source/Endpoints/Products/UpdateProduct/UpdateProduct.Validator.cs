using FastEndpoints;

namespace TodoApi.Endpoints.Products.UpdateProduct;

public class UpdateProductValidator : Validator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        // At least one field must have a value (not null or empty for strings, not null for numbers)
        RuleFor(x => x)
            .Must(HasAtLeastOneValue)
            .WithMessage("At least one field must have a value to update.");

        When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required!")
                .MinimumLength(3).WithMessage("Product name is too short!")
                .MaximumLength(50).WithMessage("Product name is too long!");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product Description is required!")
                .MinimumLength(3).WithMessage("Product Description is too short!")
                .MaximumLength(50).WithMessage("Product Description is too long!");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Manufacturer), () =>
        {
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("Product Manufacturer is required!")
                .MinimumLength(3).WithMessage("Product Manufacturer is too short!")
                .MaximumLength(50).WithMessage("Product Manufacturer is too long!");
        });

        When(x => x.Stock.HasValue, () =>
        {
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product Amount must be greater than or equal to 0!");
        });

        When(x => x.Price.HasValue, () =>
        {
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product Price must be greater than or equal to 0!");
        });
    }

    private bool HasAtLeastOneValue(UpdateProductRequest req)
    {
        return
            !string.IsNullOrWhiteSpace(req.Name) ||
            !string.IsNullOrWhiteSpace(req.Description) ||
            !string.IsNullOrWhiteSpace(req.Manufacturer) ||
            (req.Stock.HasValue) ||
            (req.Price.HasValue);
    }
}