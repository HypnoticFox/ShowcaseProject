
namespace ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;

public interface IProductRepository : IRepository<Product>
{
    Product Add(Product product);
    void Update(Product product);
    Task<Product?> GetAsync(int id);
}
