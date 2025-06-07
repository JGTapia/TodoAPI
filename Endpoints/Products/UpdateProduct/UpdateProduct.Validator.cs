using FastEndpoints;

namespace TodoApi.Endpoints.Products.UpdateProduct;

public class UpdateProductValidator : Validator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Product name is required!")
                   .MinimumLength(3).WithMessage("Product name is too short!")
                   .MaximumLength(50).WithMessage("Product name is too long!");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product Description is required!")
            .MinimumLength(3).WithMessage("Product Description is too short!")
            .MaximumLength(50).WithMessage("Product Description is too long!");
        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessage("Product Manufacturer is required!")
            .MinimumLength(3).WithMessage("Product Manufacturer is too short!")
            .MaximumLength(50).WithMessage("Product Manufacturer is too long!");
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product Amount must be greater than or equal to 0!");
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product Price must be greater than or equal to 0!");
    }
}