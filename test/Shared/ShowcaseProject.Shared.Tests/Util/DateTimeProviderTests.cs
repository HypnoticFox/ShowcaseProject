using ShowcaseProject.Shared.Util;
using ShowcaseProject.Shared.Util.DateTimeProviderClasses;

namespace ShowcaseProject.Shared.Tests.Util;

public sealed class DateTimeProviderTests
{
    public static IEnumerable<object[]> AsyncLocalTest_TestData =>
    new List<object[]>
    {
        new object[] { new DateTimeOffset(new DateTime(2003, 2, 1)), new List<DateTimeOffset>() { new DateTimeOffset(new DateTime(1980, 3, 4)), new DateTimeOffset(new DateTime(2000, 8, 9)), new DateTimeOffset(new DateTime(2020, 5, 7)), } },
        new object[] { new DateTimeOffset(new DateTime(2004, 4, 3)), new List<DateTimeOffset>() { new DateTimeOffset(new DateTime(1981, 4, 5)), new DateTimeOffset(new DateTime(2001, 1, 2)), new DateTimeOffset(new DateTime(2021, 8, 9)), new DateTimeOffset(new DateTime(1980, 3, 4)), } },
        new object[] { new DateTimeOffset(new DateTime(2005, 6, 5)), new List<DateTimeOffset>() { new DateTimeOffset(new DateTime(1982, 7, 8)), new DateTimeOffset(new DateTime(2002, 3, 4)), new DateTimeOffset(new DateTime(2022, 1, 2)), new DateTimeOffset(new DateTime(1980, 3, 4)), new DateTimeOffset(new DateTime(2000, 8, 9)), } },
        new object[] { new DateTimeOffset(new DateTime(2006, 8, 7)), new List<DateTimeOffset>() { new DateTimeOffset(new DateTime(1983, 9, 10)), new DateTimeOffset(new DateTime(2003, 5, 6)), new DateTimeOffset(new DateTime(2023, 3, 4)), new DateTimeOffset(new DateTime(1980, 3, 4)), new DateTimeOffset(new DateTime(2000, 8, 9)), new DateTimeOffset(new DateTime(2020, 5, 7)), } },
    };

    [Theory]
    [MemberData(nameof(AsyncLocalTest_TestData))]
    public async Task AsyncLocalTest(DateTimeOffset defaultDateTimeOffset, List<DateTimeOffset> testTimes)
    {
        DateTimeProvider.CurrentDateTimeProvider = new StaticDateTimeProvider(defaultDateTimeOffset);

        var timeStart = DateTimeProvider.Now;

        var timeTasks = testTimes.ConvertAll(testTime => TimeReturner(testTime));

        var timeWhileRunning = DateTimeProvider.Now;

        await Task.WhenAll(timeTasks);

        var timeTasksResults = timeTasks.ConvertAll(task => task.Result);

        Assert.True(timeStart == DateTimeProvider.Now);
        Assert.True(timeWhileRunning == DateTimeProvider.Now);
        Assert.Equal(testTimes, timeTasksResults);
    }

    private static async Task<DateTimeOffset> TimeReturner(DateTimeOffset newTime)
    {
        using (new OverrideDateTimeProvider(newTime))
        {
            await Task.Delay(10);
            return DateTimeProvider.Now;
        }
    }
}