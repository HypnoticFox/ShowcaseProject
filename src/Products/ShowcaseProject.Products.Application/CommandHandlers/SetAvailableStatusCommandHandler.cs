
namespace ShowcaseProject.Products.Application.CommandHandlers;

public sealed class SetAvailableStatusCommandHandler : ICommandHandler<SetAvailableStatusCommand>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<SetAvailableStatusCommandHandler> _logger;

    public SetAvailableStatusCommandHandler(IProductRepository repository, ILogger<SetAvailableStatusCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(SetAvailableStatusCommand command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId);

        if (product is null)
        {
            _logger.LogWarning("No Product found with Id: {ProductId}", command.ProductId);
            throw new Exception($"No Product found with Id: {command.ProductId}");
        }

        product.SetAvailableStatus();

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}