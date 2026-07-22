using UnitConversionTool.Api.Models.Requests;
using UnitConversionTool.Api.Services;

namespace UnitConversionTool.Tests;

public class UnitConversionServiceTests
{
    private readonly UnitConversionService _service = new();

    [Theory]
    [InlineData(1, "m", "ft", 3.28084)]
    [InlineData(1, "km", "mi", 0.621371)]
    [InlineData(12, "in", "ft", 1)]
    public void Convert_Length_ReturnsExpectedResult(double value, string from, string to, double expected)
    {
        var result = _service.Convert(new ConvertRequest { Value = value, From = from, To = to });

        Assert.Equal(expected, result.Result, precision: 5);
        Assert.Equal("length", result.Category);
    }

    [Theory]
    [InlineData(0, "c", "f", 32)]
    [InlineData(100, "c", "f", 212)]
    [InlineData(0, "c", "k", 273.15)]
    [InlineData(32, "f", "c", 0)]
    public void Convert_Temperature_ReturnsExpectedResult(double value, string from, string to, double expected)
    {
        var result = _service.Convert(new ConvertRequest { Value = value, From = from, To = to });

        Assert.Equal(expected, result.Result, precision: 2);
        Assert.Equal("temperature", result.Category);
    }

    [Theory]
    [InlineData(1, "kg", "lb", 2.20462)]
    [InlineData(16, "oz", "lb", 1)]
    public void Convert_Weight_ReturnsExpectedResult(double value, string from, string to, double expected)
    {
        var result = _service.Convert(new ConvertRequest { Value = value, From = from, To = to });

        Assert.Equal(expected, result.Result, precision: 5);
        Assert.Equal("weight", result.Category);
    }

    [Theory]
    [InlineData(1, "l", "ml", 1000)]
    [InlineData(1, "gal", "l", 3.785412)]
    public void Convert_Volume_ReturnsExpectedResult(double value, string from, string to, double expected)
    {
        var result = _service.Convert(new ConvertRequest { Value = value, From = from, To = to });

        Assert.Equal(expected, result.Result, precision: 5);
        Assert.Equal("volume", result.Category);
    }

    [Fact]
    public void Convert_UnsupportedUnit_ThrowsArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            _service.Convert(new ConvertRequest { Value = 1, From = "xyz", To = "m" }));

        Assert.Contains("Unsupported source unit", exception.Message);
    }

    [Fact]
    public void Convert_CrossCategory_ThrowsArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            _service.Convert(new ConvertRequest { Value = 1, From = "m", To = "kg" }));

        Assert.Contains("different categories", exception.Message);
    }

    [Fact]
    public void GetSupportedUnits_ReturnsAllCategories()
    {
        var categories = _service.GetSupportedUnits().Select(category => category.Category).ToList();

        Assert.Contains("length", categories);
        Assert.Contains("temperature", categories);
        Assert.Contains("weight", categories);
        Assert.Contains("volume", categories);
    }
}
