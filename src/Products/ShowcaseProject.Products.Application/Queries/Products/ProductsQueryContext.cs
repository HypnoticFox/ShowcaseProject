
using LinqToDB;

namespace ShowcaseProject.Products.Application.Queries.Products;

public sealed class ProductsQueryContext : DataContext, IProductsQueryContext
{
    public ProductsQueryContext(DataOptions options)
        : base(options)
    {
    }

    public ITable<ProductDto> Products => this.GetTable<ProductDto>();
}