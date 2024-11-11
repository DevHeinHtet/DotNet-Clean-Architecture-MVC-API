using Domain.Shared;
using MediatR;

namespace Application.Products.Commands.Create;

public sealed record CreateProductCommand(string Name, decimal Price, int Quantity) : IRequest<Result<int>>;
