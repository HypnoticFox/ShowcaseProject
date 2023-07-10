
namespace ShowcaseProject.Products.Application.Queries.Products;

public interface IProductQueries
{
    Task<ProductDto?> GetProductAsync(int productId);
    Task<List<ProductDto>> GetAllProductsAsync(bool availableOnly = false);
}
