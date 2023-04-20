using LinqToDB;
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;
using System.Security.Cryptography;

namespace ShowcaseProject.Products.Application.Queries.Products;

public sealed class ProductQueries : IProductQueries
{
    private readonly IProductsQueryContext db;

    public ProductQueries(IProductsQueryContext context)
    {
        db = context;
    }

    public Task<ProductDto?> GetProductAsync(int productId)
    {
        var query = db.Products
                        .Where(p => p.Id == productId);

        return query.FirstOrDefaultAsync();
    }

    public Task<List<ProductDto>> GetAllProductsAsync(bool availableOnly = false)
    {
        var query = db.Products.AsQueryable();
        if (availableOnly)
        {
            query = query.Where(p => p.ProductStatusId == ProductStatus.Available.Id);
        }

        return query.ToListAsync();
    }
}