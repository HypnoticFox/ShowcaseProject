using LinqToDB.Mapping;

namespace ShowcaseProject.Products.Application.Queries.Products;

[Table("products")]
public sealed class ProductDto
{
    [PrimaryKey]
    public int Id { get; set; }

    [Column]
    public string Name { get; set; } = string.Empty;

    [Column]
    public string Description { get; set; } = string.Empty;

    [Column]
    public int AmountInStock { get; set; }

    [Column]
    public int ProductStatusId { get; set; }
}