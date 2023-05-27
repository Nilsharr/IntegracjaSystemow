using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.ILaptopService;
using WebClient.Interfaces;
using Laptop = WebClient.Models.Laptop;

namespace WebClient.Services;

public class SoapService : IApiMethods
{
    private readonly LaptopServiceClient _client;

    public SoapService()
    {
        _client = new LaptopServiceClient();
    }

    public async Task<IEnumerable<Laptop>> GetAllLaptops()
    {
        try
        {
            var laptops = await _client.GetAllLaptopsAsync();
            return laptops.Select(item => new Laptop(item)).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Array.Empty<Laptop>();
        }
    }

    public async Task<IEnumerable<string>> GetProducers()
    {
        try
        {
            var producers = await _client.GetProducersAsync();
            return producers ?? Array.Empty<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Array.Empty<string>();
        }
    }

    public async Task<int?> GetAmountOfLaptopsByProducer(string producer)
    {
        try
        {
            return await _client.GetAmountOfLaptopsByProducerAsync(producer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<string>> GetScreenSurfaces()
    {
        try
        {
            var screenSurfaces = await _client.GetScreenSurfacesAsync();
            return screenSurfaces ?? Array.Empty<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Array.Empty<string>();
        }
    }

    public async Task<IEnumerable<Laptop>> GetLaptopsByScreenSurface(string screenSurface)
    {
        try
        {
            var laptops = await _client.GetLaptopsByScreenSurfaceAsync(screenSurface);
            return laptops.Select(item => new Laptop(item));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Array.Empty<Laptop>();
        }
    }

    public async Task<IEnumerable<string>> GetScreenResolutions()
    {
        try
        {
            var screenResolutions = await _client.GetScreenResolutionsAsync();
            return screenResolutions ?? Array.Empty<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Array.Empty<string>();
        }
    }

    public async Task<int?> GetAmountOfLaptopsByScreenResolution(string screenResolution)
    {
        try
        {
            return await _client.GetAmountOfLaptopsByScreenResolutionAsync(screenResolution);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public Task<Laptop?> AddLaptop(Laptop laptop)
    {
        throw new NotImplementedException();
    }

    public Task<Laptop?> UpdateLaptop(int id, Laptop laptop)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLaptop(int id)
    {
        throw new NotImplementedException();
    }
}