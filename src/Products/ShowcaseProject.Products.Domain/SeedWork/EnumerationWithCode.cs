
using ShowcaseProject.Shared.Extensions;

namespace ShowcaseProject.Products.Domain.SeedWork;

public abstract class EnumerationWithCode : Enumeration
{
    public string Code { get; private set; }

    protected EnumerationWithCode(int id, string code, string name)
        : base(id, name)
    {
        Code = code.ThrowIfNullOrEmpty();
    }

    public static T FromCode<T>(string code) where T : EnumerationWithCode
    {
        var matchingItem = Parse<T, string>(code, "code", item => item.Code == code);
        return matchingItem;
    }
}
