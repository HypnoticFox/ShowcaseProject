
namespace ShowcaseProject.Products.Application.Queries.Products;

public sealed record ProductDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int AmountInStock { get; init; }

    public int ProductStatusId { get; init; }
}