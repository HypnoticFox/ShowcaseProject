
namespace ShowcaseProject.Shared.Util.DateTimeProviderClasses;

public sealed class OverrideDateTimeProvider : IDisposable
{
    private readonly IDateTimeProvider _originalProvider;
    private readonly StaticDateTimeProvider _staticProvider;

    private OverrideDateTimeProvider(StaticDateTimeProvider staticProvider)
    {
        _staticProvider = staticProvider;
        _originalProvider = DateTimeProvider.CurrentDateTimeProvider;

        DateTimeProvider.CurrentDateTimeProvider = _staticProvider;
    }

    public OverrideDateTimeProvider()
        : this(new StaticDateTimeProvider())
    {
    }

    public OverrideDateTimeProvider(DateTimeOffset now)
        : this(new StaticDateTimeProvider(now))
    {
    }

    public void Dispose()
    {
        DateTimeProvider.CurrentDateTimeProvider = _originalProvider;
        GC.SuppressFinalize(this);
    }

    public OverrideDateTimeProvider SetNow(DateTimeOffset now)
    {
        _staticProvider.SetNow(now);
        return this;
    }

    public OverrideDateTimeProvider MoveTimeForward(TimeSpan amount)
    {
        _staticProvider.MoveTimeForward(amount);
        return this;
    }
}
