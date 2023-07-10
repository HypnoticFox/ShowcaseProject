
namespace ShowcaseProject.Products.Application.CommandHandlers;

public sealed class RemoveFromStockCommandHandler : ICommandHandler<RemoveFromStockCommand>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<RemoveFromStockCommandHandler> _logger;

    public RemoveFromStockCommandHandler(IProductRepository repository, ILogger<RemoveFromStockCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(RemoveFromStockCommand command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId);

        if (product is null)
        {
            _logger.LogWarning("No Product found with Id: {ProductId}", command.ProductId);
            throw new Exception($"No Product found with Id: {command.ProductId}");
        }

        product.RemoveStock(command.AmountToRemove);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}