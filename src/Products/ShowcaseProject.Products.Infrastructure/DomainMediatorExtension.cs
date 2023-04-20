
using ShowcaseProject.Products.Domain.SeedWork;

namespace ShowcaseProject.Products.Infrastructure;

static class DomainMediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IDomainMediator mediator, ProductsContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents?.Any() == true);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}