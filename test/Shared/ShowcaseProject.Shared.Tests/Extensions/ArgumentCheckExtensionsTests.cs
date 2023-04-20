using ShowcaseProject.Shared.Extensions;
using System.Collections;

namespace ShowcaseProject.Shared.Tests.Extensions;

public sealed class ArgumentCheckExtensionsTests
{
    #region object.ThrowIfNull() tests

    [Theory]
    [InlineData(null, "The string may not be null!")]
    [InlineData(null, "NULL!!!")]
    [InlineData(null)]
    public void ThrowIfNull_DoesThrow(object testParameter, string? testExceptionMessage = null)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => testParameter.ThrowIfNull(testExceptionMessage));

        if (testExceptionMessage is null) return;

        Assert.Contains(testExceptionMessage, exception.Message);
        Assert.True(exception.ParamName == nameof(testParameter));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(123)]
    [InlineData(0.123)]
    [InlineData("")]
    [InlineData("123")]
    public void ThrowIfNull_DoesNotThrow(object testParameter)
    {
        testParameter.ThrowIfNull();
    }

    #endregion

    #region string.ThrowIfNullOrEmpty() tests

    [Theory]
    [InlineData(null, "The string may not be null!")]
    [InlineData(null, "NULL!!!")]
    [InlineData(null)]
    public void String_ThrowIfNullOrEmpty_DoesThrowArgumentNullException(string? testParameter, string? testExceptionMessage = null)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => testParameter.ThrowIfNullOrEmpty(testExceptionMessage));

        if (testExceptionMessage is null) return;

        Assert.Contains(testExceptionMessage, exception.Message);
        Assert.True(exception.ParamName == nameof(testParameter));
    }

    [Theory]
    [InlineData("", "The string may not be null!")]
    [InlineData("", "NULL!!!")]
    [InlineData("")]
    public void String_ThrowIfNullOrEmpty_DoesThrowArgumentException(string? testParameter, string? testExceptionMessage = null)
    {
        var exception = Assert.Throws<ArgumentException>(() => testParameter.ThrowIfNullOrEmpty(testExceptionMessage));

        if (testExceptionMessage is null) return;

        Assert.Contains(testExceptionMessage, exception.Message);
        Assert.True(exception.ParamName == nameof(testParameter));
    }

    [Theory]
    [InlineData("123")]
    [InlineData("abcde")]
    [InlineData("null")]
    public void String_ThrowIfNullOrEmpty_DoesNotThrow(string? testParameter, string? testExceptionMessage = null)
    {
        testParameter.ThrowIfNullOrEmpty(testExceptionMessage);
    }

    #endregion

    #region int.ThrowIfNegative() tests

    [Theory]
    [InlineData(null, "The integer may not be null!")]
    [InlineData(null, "NULL!!!")]
    [InlineData(null)]
    public void Int_ThrowIfNegative_DoesThrowArgumentNullException(int? testParameter, string? testExceptionMessage = null)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => testParameter.ThrowIfNegative(testExceptionMessage));

        if (testExceptionMessage is null) return;

        Assert.Contains(testExceptionMessage, exception.Message);
        Assert.True(exception.ParamName == nameof(testParameter));
    }

    [Theory]
    [InlineData(-1, "The integer may not negative!")]
    [InlineData(-999, "NEGATIVE!!!")]
    [InlineData(-123456)]
    [InlineData(int.MinValue)]
    public void Int_ThrowIfNegative_DoesThrowOutOfRangeException(int testParameter, string? testExceptionMessage = null)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => testParameter.ThrowIfNegative(testExceptionMessage));

        if (testExceptionMessage is null) return;

        Assert.Contains(testExceptionMessage, exception.Message);
        Assert.True(exception.ParamName == nameof(testParameter));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(123)]
    [InlineData(9999)]
    [InlineData(int.MaxValue)]
    public void Int_ThrowIfNegative_DoesNotThrow(int? testParameter, string? testExceptionMessage = null)
    {
        testParameter.ThrowIfNegative(testExceptionMessage);
    }

    #endregion

    #region IEnumerable.ThrowIfNullOrEmpty() tests

    [Theory]
    [InlineData(null, "The collection may not be null!")]
    [InlineData(null, "NULL!!!")]
    [InlineData(null)]
    public void IEnumerable_ThrowIfNullOrEmpty_DoesThrowArgumentNullException(IEnumerable<object>? testParameter, string? testExceptionMessage = null)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => testParameter.ThrowIfNullOrEmpty(testExceptionMessage));

        if (testExceptionMessage is null) return;

        Assert.Contains(testExceptionMessage, exception.Message);
        Assert.True(exception.ParamName == nameof(testParameter));
    }

    public static IEnumerable<object[]> IEnumerable_ThrowIfNullOrEmpty_EmptyTestData =>
    new List<object[]>
    {
        new object[] { Array.Empty<int>(), "The collection may not be null!" },
        new object[] { new List<int>(), "This one is Empty" },
        new object[] { new Queue<int>(), "Empty" },
        new object[] { new Stack<int>() },
    };

    [Theory]
    [MemberData(nameof(IEnumerable_ThrowIfNullOrEmpty_EmptyTestData))]
    public void IEnumerable_ThrowIfNullOrEmpty_DoesThrowArgumentException(IEnumerable<int>? testParameter, string? testExceptionMessage = null)
    {
        var exception = Assert.Throws<ArgumentException>(() => testParameter.ThrowIfNullOrEmpty(testExceptionMessage));

        if (testExceptionMessage is null) return;

        Assert.Contains(testExceptionMessage, exception.Message);
        Assert.True(exception.ParamName == nameof(testParameter));
    }

    public static IEnumerable<object[]> IEnumerable_ThrowIfNullOrEmpty_FilledTestData =>
    new List<object[]>
    {
        new object[] { new int[] { 1 } },
        new object[] { new List<int>() { 1, 2 } },
        new object[] { new Queue<int>(new int[] { 1, 2, 3 }) },
        new object[] { new Stack<int>(new int[] { 1, 2, 3, 4 }) },
    };

    [Theory]
    [MemberData(nameof(IEnumerable_ThrowIfNullOrEmpty_FilledTestData))]
    public void IEnumerable_ThrowIfNullOrEmpty_DoesNotThrow(IEnumerable<int>? testParameter, string? testExceptionMessage = null)
    {
        testParameter.ThrowIfNullOrEmpty(testExceptionMessage);
    }

    #endregion
}
