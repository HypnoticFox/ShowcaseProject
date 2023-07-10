
namespace ShowcaseProject.Products.Application.CommandHandlers;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IProductRepository repository, ILogger<CreateProductCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var newProduct = new Product(command.Name, command.Description, command.AmountInStock);

        _repository.Add(newProduct);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return newProduct.Id;
    }
}