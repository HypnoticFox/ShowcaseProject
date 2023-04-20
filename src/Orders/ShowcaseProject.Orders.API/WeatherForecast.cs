namespace ShowcaseProject.Orders.API;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public void Test()
    {
        var teststring = "test";
        teststring.ThrowIfNull();
        teststring.ThrowIfNullOrEmpty("iets");

        int? number = 1;
        number.ThrowIfNull();
        number.ThrowIfNegative();

        var testList = new List<int> { 1, 2, 3 };
        testList.ThrowIfNullOrEmpty();

        var testArray = new string[] {"a", "b", "c"};
        testArray.ThrowIfNullOrEmpty();

        var test = DateTime.Now;
        test = DateTime.UtcNow;
        test = DateTime.UtcNow.AddYears(5);

        var test2 = DateTimeOffset.Now;
        var test3 = DateTimeProvider.Now;
    }
}