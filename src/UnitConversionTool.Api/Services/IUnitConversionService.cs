using UnitConversionTool.Api.Models.Requests;
using UnitConversionTool.Api.Models.Responses;

namespace UnitConversionTool.Api.Services;

public interface IUnitConversionService
{
    ConvertResponse Convert(ConvertRequest request);

    IReadOnlyList<CategoryUnitsResponse> GetSupportedUnits();
}
