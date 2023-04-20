
namespace ShowcaseProject.Shared.Util.DateTimeProviderClasses;

public sealed class StaticDateTimeProvider : IDateTimeProvider
{
    private DateTimeOffset _now;

    public StaticDateTimeProvider()
    {
        _now = DateTimeOffset.UtcNow;
    }

    public StaticDateTimeProvider(DateTimeOffset now)
    {
        _now = now;
    }

    public DateTimeOffset Now
    {
        get { return _now; }
    }

    public StaticDateTimeProvider SetNow(DateTimeOffset now)
    {
        _now = now;
        return this;
    }

    public StaticDateTimeProvider MoveTimeForward(TimeSpan amount)
    {
        _now = _now.Add(amount);
        return this;
    }
}
