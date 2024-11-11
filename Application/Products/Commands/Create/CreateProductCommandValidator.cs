using Domain.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Name field is required.")
               .MustAsync(BeNameUnique)
               .WithMessage("Name is already taken.")
               .MaximumLength(100);
        }

        private async Task<bool> BeNameUnique(string name, CancellationToken cancellationToken)
        {
            return !await _productRepository.IsExist(name);
        }
    }
}
