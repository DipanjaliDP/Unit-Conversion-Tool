using UnitConversionTool.Api.Models;

namespace UnitConversionTool.Api.Data;

public static class UnitRegistry
{
    private static readonly IReadOnlyList<UnitDefinition> Units =
    [
        // Length (base: meter)
        new() { Code = "m", Name = "Meter", Category = ConversionCategory.Length, ToBaseFactor = 1d },
        new() { Code = "km", Name = "Kilometer", Category = ConversionCategory.Length, ToBaseFactor = 1000d },
        new() { Code = "cm", Name = "Centimeter", Category = ConversionCategory.Length, ToBaseFactor = 0.01d },
        new() { Code = "mm", Name = "Millimeter", Category = ConversionCategory.Length, ToBaseFactor = 0.001d },
        new() { Code = "in", Name = "Inch", Category = ConversionCategory.Length, ToBaseFactor = 0.0254d },
        new() { Code = "ft", Name = "Foot", Category = ConversionCategory.Length, ToBaseFactor = 0.3048d },
        new() { Code = "yd", Name = "Yard", Category = ConversionCategory.Length, ToBaseFactor = 0.9144d },
        new() { Code = "mi", Name = "Mile", Category = ConversionCategory.Length, ToBaseFactor = 1609.344d },

        // Temperature (factor unused; conversions handled separately)
        new() { Code = "c", Name = "Celsius", Category = ConversionCategory.Temperature, ToBaseFactor = 1d },
        new() { Code = "f", Name = "Fahrenheit", Category = ConversionCategory.Temperature, ToBaseFactor = 1d },
        new() { Code = "k", Name = "Kelvin", Category = ConversionCategory.Temperature, ToBaseFactor = 1d },

        // Weight / mass (base: kilogram)
        new() { Code = "kg", Name = "Kilogram", Category = ConversionCategory.Weight, ToBaseFactor = 1d },
        new() { Code = "g", Name = "Gram", Category = ConversionCategory.Weight, ToBaseFactor = 0.001d },
        new() { Code = "mg", Name = "Milligram", Category = ConversionCategory.Weight, ToBaseFactor = 0.000001d },
        new() { Code = "lb", Name = "Pound", Category = ConversionCategory.Weight, ToBaseFactor = 0.45359237d },
        new() { Code = "oz", Name = "Ounce", Category = ConversionCategory.Weight, ToBaseFactor = 0.028349523125d },

        // Volume (base: liter)
        new() { Code = "l", Name = "Liter", Category = ConversionCategory.Volume, ToBaseFactor = 1d },
        new() { Code = "ml", Name = "Milliliter", Category = ConversionCategory.Volume, ToBaseFactor = 0.001d },
        new() { Code = "gal", Name = "US Gallon", Category = ConversionCategory.Volume, ToBaseFactor = 3.785411784d },
        new() { Code = "qt", Name = "US Quart", Category = ConversionCategory.Volume, ToBaseFactor = 0.946352946d },
        new() { Code = "pt", Name = "US Pint", Category = ConversionCategory.Volume, ToBaseFactor = 0.473176473d },
        new() { Code = "cup", Name = "US Cup", Category = ConversionCategory.Volume, ToBaseFactor = 0.2365882365d },
        new() { Code = "floz", Name = "US Fluid Ounce", Category = ConversionCategory.Volume, ToBaseFactor = 0.0295735295625d }
    ];

    private static readonly Dictionary<string, UnitDefinition> UnitsByCode =
        Units.ToDictionary(unit => unit.Code, StringComparer.OrdinalIgnoreCase);

    public static bool TryGetUnit(string code, out UnitDefinition unit) =>
        UnitsByCode.TryGetValue(code.Trim(), out unit!);

    public static IReadOnlyList<UnitDefinition> GetAllUnits() => Units;

    public static IEnumerable<IGrouping<ConversionCategory, UnitDefinition>> GetUnitsByCategory() =>
        Units.GroupBy(unit => unit.Category);
}
