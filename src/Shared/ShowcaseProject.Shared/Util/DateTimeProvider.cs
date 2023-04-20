using ShowcaseProject.Shared.Util.DateTimeProviderClasses;

namespace ShowcaseProject.Shared.Util;

public static class DateTimeProvider
{
    private static readonly AsyncLocal<IDateTimeProvider> _asyncLocalDateTimeProvider = new();
    private static Type _defaultDateTimeProvider = typeof(UtcDateTimeProvider);
    public static Type DefaultDateTimeProvider
    {
        set
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (!typeof(IDateTimeProvider).IsAssignableFrom(value)) throw new ArgumentException($"{value.Name} does not implement {nameof(IDateTimeProvider)}", nameof(value));
            _defaultDateTimeProvider = value;
        }
        get { return _defaultDateTimeProvider; }
    }

    public static IDateTimeProvider CurrentDateTimeProvider
    {
        get
        {
            if (_asyncLocalDateTimeProvider.Value is null) ResetDateTimeProvider();
            return _asyncLocalDateTimeProvider.Value!;
        }
        set => _asyncLocalDateTimeProvider.Value = value;
    }

    public static DateTimeOffset Now => CurrentDateTimeProvider.Now;
    public static DateTime LocalNow => CurrentDateTimeProvider.Now.LocalDateTime;
    public static DateTime UtcNow => CurrentDateTimeProvider.Now.UtcDateTime;

    public static void ResetDateTimeProvider()
    {
        _asyncLocalDateTimeProvider.Value = (IDateTimeProvider)Activator.CreateInstance(_defaultDateTimeProvider)!;
    }

    /*private static void ProviderChanged(AsyncLocalValueChangedArgs<IDateTimeProvider> args)
    {
        if (args.ThreadContextChanged)
        {
            Debug.WriteLine($"ThreadContext has changed. Provider changed from {args.PreviousValue?.Now.ToString() ?? "null"} to {args.CurrentValue?.Now.ToString() ?? "null"}");
        }
        else
        {
            Debug.WriteLine($"Provider changed from {args.PreviousValue?.Now.ToString() ?? "null"} to {args.CurrentValue?.Now.ToString() ?? "null"}");
        }
    }*/
}
