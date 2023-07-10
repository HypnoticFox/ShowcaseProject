
namespace ShowcaseProject.Products.Application.CommandHandlers;

public sealed class SetDiscontinuedStatusCommandHandler : ICommandHandler<SetDiscontinuedStatusCommand>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<SetDiscontinuedStatusCommandHandler> _logger;

    public SetDiscontinuedStatusCommandHandler(IProductRepository repository, ILogger<SetDiscontinuedStatusCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(SetDiscontinuedStatusCommand command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId);

        if (product is null)
        {
            _logger.LogWarning("No Product found with Id: {ProductId}", command.ProductId);
            throw new Exception($"No Product found with Id: {command.ProductId}");
        }

        product.SetDiscontinuedStatus();

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}