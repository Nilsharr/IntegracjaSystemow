using Microsoft.EntityFrameworkCore;
using SoapServer.Database.Context;
using SoapServer.Dto;
using SoapServer.Interfaces;

// ReSharper disable SpecifyStringComparison

namespace SoapServer.Services;

public class LaptopService : ILaptopService, IDisposable
{
    private readonly LaptopDbContext _dbContext;

    public LaptopService()
    {
        _dbContext = new LaptopDbContext();
    }

    public async Task<IEnumerable<Laptop>> GetAllLaptops()
    {
        var laptops = await _dbContext.Laptops.ToListAsync();
        return laptops.Select(item => new Laptop(item)).ToList();
    }

    public async Task<IEnumerable<string>> GetProducers()
    {
        return await _dbContext.Laptops.Select(x => x.Producer).Distinct().ToListAsync();
    }

    public async Task<int> GetAmountOfLaptopsByProducer(string producer)
    {
        return await _dbContext.Laptops.CountAsync(x => x.Producer.ToLower() == producer.ToLower());
    }

    public async Task<IEnumerable<string>> GetScreenSurfaces()
    {
        return (await _dbContext.Laptops.Where(x => x.ScreenSurface != null).Select(x => x.ScreenSurface).Distinct()
            .ToListAsync())!;
    }

    public async Task<IEnumerable<Laptop>> GetLaptopsByScreenSurface(string screenSurface)
    {
        var laptops = await _dbContext.Laptops
            .Where(x => x.ScreenSurface != null && x.ScreenSurface.ToLower() == screenSurface.ToLower())
            .ToListAsync();
        return laptops.Select(item => new Laptop(item)).ToList();
    }

    public async Task<IEnumerable<string>> GetScreenResolutions()
    {
        return (await _dbContext.Laptops.Where(x => x.ScreenResolution != null).Select(x => x.ScreenResolution)
            .Distinct().ToListAsync())!;
    }

    public async Task<int> GetAmountOfLaptopsByScreenResolution(string screenResolution)
    {
        return await _dbContext.Laptops.CountAsync(x =>
            x.ScreenResolution != null && x.ScreenResolution.ToLower() == screenResolution.ToLower());
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}