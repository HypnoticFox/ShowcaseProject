
namespace ShowcaseProject.Shared.Util.DateTimeProviderClasses;

public sealed class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
