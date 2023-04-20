
namespace ShowcaseProject.Shared.Util.DateTimeProviderClasses;

public sealed class LocalDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
