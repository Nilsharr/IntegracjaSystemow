using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestServer.Database.Context;
using RestServer.Dto;

// ReSharper disable SpecifyStringComparison

namespace RestServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LaptopsController : ControllerBase
{
    private readonly LaptopDbContext _laptopDbContext;

    public LaptopsController(LaptopDbContext laptopDbContext)
    {
        _laptopDbContext = laptopDbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LaptopDto>>> GetAll([FromQuery] string? screenSurface = null)
    {
        var laptopQuery = _laptopDbContext.Laptops.AsQueryable();
        if (screenSurface is not null)
        {
            laptopQuery = laptopQuery.Where(x =>
                x.ScreenSurface != null && x.ScreenSurface.ToLower() == screenSurface.ToLower());
        }

        var laptops = await laptopQuery.ToListAsync();
        return Ok(laptops.Select(item => new LaptopDto(item)).ToList());
    }

    [HttpGet("producers")]
    public async Task<ActionResult<IEnumerable<string>>> GetProducers()
    {
        var producers = await _laptopDbContext.Laptops.Select(x => x.Producer).Distinct().ToListAsync();
        return Ok(producers);
    }

    [HttpGet("producers/amount")]
    public async Task<ActionResult<int>> GetAmountOfLaptopsByProducer([FromQuery] string producer)
    {
        var amountByProducer =
            await _laptopDbContext.Laptops.CountAsync(x => x.Producer.ToLower() == producer.ToLower());
        return Ok(amountByProducer);
    }

    [HttpGet("screen-surface")]
    public async Task<ActionResult<IEnumerable<string>>> GetScreenSurfaces()
    {
        var screenSurfaces = await _laptopDbContext.Laptops.Where(x => x.ScreenSurface != null)
            .Select(x => x.ScreenSurface).Distinct().ToListAsync();
        return Ok(screenSurfaces);
    }

    [HttpGet("screen-resolution")]
    public async Task<ActionResult<IEnumerable<string>>> GetScreenResolutions()
    {
        var producers = await _laptopDbContext.Laptops.Where(x => x.ScreenResolution != null)
            .Select(x => x.ScreenResolution).Distinct().ToListAsync();
        return Ok(producers);
    }

    [HttpGet("screen-resolution/amount")]
    public async Task<ActionResult<int>> GetAmountOfLaptopsByScreenResolution([FromQuery] string screenResolution)
    {
        var amountByProducer = await _laptopDbContext.Laptops.CountAsync(x =>
            x.ScreenResolution != null && x.ScreenResolution.ToLower() == screenResolution.ToLower());
        return Ok(amountByProducer);
    }
}