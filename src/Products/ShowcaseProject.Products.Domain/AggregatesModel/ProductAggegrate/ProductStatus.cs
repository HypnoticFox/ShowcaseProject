
using ShowcaseProject.Products.Domain.Exceptions;

namespace ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;

public sealed class ProductStatus : EnumerationWithCode
{
    public static readonly ProductStatus Concept = new(1, nameof(Concept).ToLowerInvariant(), "Concept");
    public static readonly ProductStatus Available = new(2, nameof(Available).ToLowerInvariant(), "Available");
    public static readonly ProductStatus Unavailable = new(3, nameof(Unavailable).ToLowerInvariant(), "Unavailable");
    public static readonly ProductStatus Discontinued = new(4, nameof(Discontinued).ToLowerInvariant(), "Discontinued");

    private ProductStatus(int id, string code, string name)
        : base(id, code, name)
    {
    }

    public static IEnumerable<ProductStatus> List() =>
        GetAll<ProductStatus>();

    public static ProductStatus FromId(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new ProductDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static ProductStatus FromCode(string name)
    {
        var state = List()
            .SingleOrDefault(s => String.Equals(s.Code, name, StringComparison.InvariantCultureIgnoreCase));

        if (state == null)
        {
            throw new ProductDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}