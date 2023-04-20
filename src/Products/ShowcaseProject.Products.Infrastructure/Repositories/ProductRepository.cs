
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;
using ShowcaseProject.Products.Domain.SeedWork;

namespace ShowcaseProject.Products.Infrastructure.Repositories;

public sealed class ProductRepository
    : IProductRepository
{
    private readonly ProductsContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public ProductRepository(ProductsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Product Add(Product product)
    {
        return _context.Products.Add(product).Entity;

    }

    public void Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
    }

    public async Task<Product?> GetAsync(int productId)
    {
        var product = await _context
                            .Products
                            .FirstOrDefaultAsync(o => o.Id == productId);

        product ??= _context
                        .Products
                        .Local
                        .FirstOrDefault(o => o.Id == productId);

        return product;
    }
}
