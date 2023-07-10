
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;
using SqlKata.Execution;

namespace ShowcaseProject.Products.Application.Queries.Products;

public sealed class ProductQueries : IProductQueries
{
    private readonly ProductQueryFactory db;

    public ProductQueries(ProductQueryFactory context)
    {
        db = context;
    }

    public async Task<ProductDto?> GetProductAsync(int productId)
    {
        var query = db.Query("products.products")
            .Where("Id", productId);

        var result = await query.FirstOrDefaultAsync<ProductDto>();
        return result;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync(bool availableOnly = false)
    {
        var query = db.Query("products.products");
        if (availableOnly)
        {
            query = query.Where("ProductStatusId", ProductStatus.Available.Id);
        }

        var result = await query.GetAsync<ProductDto>();

        return result.ToList();
    }
}