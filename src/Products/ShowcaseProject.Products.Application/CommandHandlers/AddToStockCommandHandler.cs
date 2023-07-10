
namespace ShowcaseProject.Products.Application.CommandHandlers;

public sealed class AddToStockCommandHandler : ICommandHandler<AddToStockCommand>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<AddToStockCommandHandler> _logger;

    public AddToStockCommandHandler(IProductRepository repository, ILogger<AddToStockCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(AddToStockCommand command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId);

        if (product is null)
        {
            _logger.LogWarning("No Product found with Id: {ProductId}", command.ProductId);
            throw new Exception($"No Product found with Id: {command.ProductId}");
        }

        product.AddStock(command.AmountToAdd);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}