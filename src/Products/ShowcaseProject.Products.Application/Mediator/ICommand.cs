
using MediatR;

namespace ShowcaseProject.Products.Application.Mediator;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
