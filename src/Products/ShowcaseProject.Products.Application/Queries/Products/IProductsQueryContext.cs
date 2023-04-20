
using LinqToDB;

namespace ShowcaseProject.Products.Application.Queries.Products;

public interface IProductsQueryContext : IDataContext
{
    public ITable<ProductDto> Products { get; }
}
