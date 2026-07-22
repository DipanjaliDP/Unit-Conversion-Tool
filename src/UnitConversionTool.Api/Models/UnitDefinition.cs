namespace UnitConversionTool.Api.Models;

public sealed class UnitDefinition
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required ConversionCategory Category { get; init; }
    public required double ToBaseFactor { get; init; }
}
