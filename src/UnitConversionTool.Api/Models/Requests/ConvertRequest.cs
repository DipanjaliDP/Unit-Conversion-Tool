using System.ComponentModel.DataAnnotations;

namespace UnitConversionTool.Api.Models.Requests;

public sealed class ConvertRequest
{
    [Required]
    public double Value { get; init; }

    [Required]
    [MinLength(1)]
    public required string From { get; init; }

    [Required]
    [MinLength(1)]
    public required string To { get; init; }
}
