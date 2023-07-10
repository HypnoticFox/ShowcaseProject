
namespace ShowcaseProject.Products.Application.Queries.Products;

public sealed record ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int AmountInStock { get; set; }

    public int ProductStatusId { get; set; }
}