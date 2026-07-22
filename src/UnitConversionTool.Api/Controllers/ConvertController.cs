using Microsoft.AspNetCore.Mvc;
using UnitConversionTool.Api.Models.Requests;
using UnitConversionTool.Api.Models.Responses;
using UnitConversionTool.Api.Services;

namespace UnitConversionTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ConvertController(IUnitConversionService conversionService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ConvertResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<ConvertResponse> Convert([FromBody] ConvertRequest request)
    {
        try
        {
            return Ok(conversionService.Convert(request));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Conversion failed",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}
