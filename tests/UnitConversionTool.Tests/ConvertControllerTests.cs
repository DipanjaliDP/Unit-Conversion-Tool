using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using UnitConversionTool.Api.Models.Requests;
using UnitConversionTool.Api.Models.Responses;

namespace UnitConversionTool.Tests;

public class ConvertControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ConvertControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Convert_ValidRequest_ReturnsOk()
    {
        var response = await _client.PostAsJsonAsync("/api/convert", new ConvertRequest
        {
            Value = 10,
            From = "m",
            To = "ft"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<ConvertResponse>();
        Assert.NotNull(body);
        Assert.Equal(10, body.InputValue);
        Assert.Equal("m", body.FromUnit);
        Assert.Equal("ft", body.ToUnit);
        Assert.Equal("length", body.Category);
    }

    [Fact]
    public async Task Convert_InvalidUnit_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync("/api/convert", new ConvertRequest
        {
            Value = 10,
            From = "invalid",
            To = "m"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetUnits_ReturnsSupportedUnits()
    {
        var response = await _client.GetAsync("/api/units");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<List<CategoryUnitsResponse>>();
        Assert.NotNull(body);
        Assert.True(body.Count >= 4);
    }
}
