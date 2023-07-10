
using MediatR;
using ShowcaseProject.Products.Domain.SeedWork;

namespace ShowcaseProject.Products.Application.Mediator;

public sealed class DomainMediator : IDomainMediator
{
    private readonly IPublisher _publisher;
    public DomainMediator(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Publish<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : IDomainNotification
    {
        return _publisher.Publish(notification, cancellationToken);
    }
}