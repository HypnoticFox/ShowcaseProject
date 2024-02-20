using ShowcaseProject.Products.Application.Queries.Products;

namespace ShowcaseProject.Products.API.Responses;

public sealed class ProductResponse
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int AmountInStock { get; init; }

    public int ProductStatusId { get; init; }
}

public static class ProductResponseExtensions
{
    public static ProductResponse ToProductResponse(this ProductDto product)
    {
        return new ProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            AmountInStock = product.AmountInStock,
            ProductStatusId = product.ProductStatusId
        };
    }
}
