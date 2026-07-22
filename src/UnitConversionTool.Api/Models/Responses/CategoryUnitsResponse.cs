namespace UnitConversionTool.Api.Models.Responses;

public sealed class CategoryUnitsResponse
{
    public required string Category { get; init; }
    public required IReadOnlyList<UnitInfoResponse> Units { get; init; }
}
