using FluentValidation;

namespace Xero.RefactorMe.Web.ViewModels.Validation
{
    public class ProductViewModelValidation : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidation()
        {
            RuleFor(p => p.Name.Length).Must(length =>
            {
                return length > 100;
            }).WithMessage("Name must be shorter than 100");

            RuleFor(p => p.Name).NotEmpty().WithMessage("Name must not be empty");

            RuleFor(p => p.Description.Length).Must(length =>
            {
                return length > 500;
            }).WithMessage("Description must be shorter than 500"); 
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description must not be empty");
        }
    }
}