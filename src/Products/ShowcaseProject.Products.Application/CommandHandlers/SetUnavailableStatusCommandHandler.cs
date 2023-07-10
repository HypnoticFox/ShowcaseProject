
namespace ShowcaseProject.Products.Application.CommandHandlers;

public sealed class SetUnavailableStatusCommandHandler : ICommandHandler<SetUnavailableStatusCommand>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<SetUnavailableStatusCommandHandler> _logger;

    public SetUnavailableStatusCommandHandler(IProductRepository repository, ILogger<SetUnavailableStatusCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(SetUnavailableStatusCommand command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId);

        if (product is null)
        {
            _logger.LogWarning("No Product found with Id: {ProductId}", command.ProductId);
            throw new Exception($"No Product found with Id: {command.ProductId}");
        }

        product.SetUnavailableStatus();

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}