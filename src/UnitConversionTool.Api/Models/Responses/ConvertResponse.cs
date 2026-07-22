namespace UnitConversionTool.Api.Models.Responses;

public sealed class ConvertResponse
{
    public required double InputValue { get; init; }
    public required string FromUnit { get; init; }
    public required string ToUnit { get; init; }
    public required double Result { get; init; }
    public required string Category { get; init; }
}
