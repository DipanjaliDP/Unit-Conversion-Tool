using UnitConversionTool.Api.Data;
using UnitConversionTool.Api.Models;
using UnitConversionTool.Api.Models.Requests;
using UnitConversionTool.Api.Models.Responses;

namespace UnitConversionTool.Api.Services;

public sealed class UnitConversionService : IUnitConversionService
{
    public ConvertResponse Convert(ConvertRequest request)
    {
        if (!UnitRegistry.TryGetUnit(request.From, out var fromUnit))
        {
            throw new ArgumentException($"Unsupported source unit '{request.From}'.", nameof(request));
        }

        if (!UnitRegistry.TryGetUnit(request.To, out var toUnit))
        {
            throw new ArgumentException($"Unsupported target unit '{request.To}'.", nameof(request));
        }

        if (fromUnit.Category != toUnit.Category)
        {
            throw new ArgumentException(
                $"Cannot convert between different categories: '{fromUnit.Category}' and '{toUnit.Category}'.",
                nameof(request));
        }

        var result = fromUnit.Category switch
        {
            ConversionCategory.Temperature => ConvertTemperature(request.Value, fromUnit.Code, toUnit.Code),
            _ => ConvertLinear(request.Value, fromUnit.ToBaseFactor, toUnit.ToBaseFactor)
        };

        return new ConvertResponse
        {
            InputValue = request.Value,
            FromUnit = fromUnit.Code,
            ToUnit = toUnit.Code,
            Result = Math.Round(result, 6),
            Category = fromUnit.Category.ToString().ToLowerInvariant()
        };
    }

    public IReadOnlyList<CategoryUnitsResponse> GetSupportedUnits() =>
        UnitRegistry.GetUnitsByCategory()
            .Select(group => new CategoryUnitsResponse
            {
                Category = group.Key.ToString().ToLowerInvariant(),
                Units = group
                    .Select(unit => new UnitInfoResponse
                    {
                        Code = unit.Code,
                        Name = unit.Name
                    })
                    .OrderBy(unit => unit.Code, StringComparer.OrdinalIgnoreCase)
                    .ToList()
            })
            .OrderBy(category => category.Category, StringComparer.OrdinalIgnoreCase)
            .ToList();

    private static double ConvertLinear(double value, double fromFactor, double toFactor) =>
        value * fromFactor / toFactor;

    private static double ConvertTemperature(double value, string fromCode, string toCode)
    {
        var celsius = fromCode.ToLowerInvariant() switch
        {
            "c" => value,
            "f" => (value - 32d) * 5d / 9d,
            "k" => value - 273.15d,
            _ => throw new ArgumentException($"Unsupported temperature unit '{fromCode}'.", nameof(fromCode))
        };

        return toCode.ToLowerInvariant() switch
        {
            "c" => celsius,
            "f" => celsius * 9d / 5d + 32d,
            "k" => celsius + 273.15d,
            _ => throw new ArgumentException($"Unsupported temperature unit '{toCode}'.", nameof(toCode))
        };
    }
}
