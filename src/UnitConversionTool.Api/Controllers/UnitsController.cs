using Microsoft.AspNetCore.Mvc;
using UnitConversionTool.Api.Models.Responses;
using UnitConversionTool.Api.Services;

namespace UnitConversionTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UnitsController(IUnitConversionService conversionService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryUnitsResponse>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<CategoryUnitsResponse>> GetSupportedUnits() =>
        Ok(conversionService.GetSupportedUnits());
}
