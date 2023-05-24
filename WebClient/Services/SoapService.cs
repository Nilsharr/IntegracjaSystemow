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
    public async Task<IEnumerable<Laptop>> GetAllLaptops()
    {
        await using var client = new LaptopServiceClient();
        try
        {
            var laptops = await client.GetAllLaptopsAsync();
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
        await using var client = new LaptopServiceClient();
        try
        {
            var producers = await client.GetProducersAsync();
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
        await using var client = new LaptopServiceClient();
        try
        {
            return await client.GetAmountOfLaptopsByProducerAsync(producer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<string>> GetScreenSurfaces()
    {
        await using var client = new LaptopServiceClient();
        try
        {
            var screenSurfaces = await client.GetScreenSurfacesAsync();
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
        await using var client = new LaptopServiceClient();
        try
        {
            var laptops = await client.GetLaptopsByScreenSurfaceAsync(screenSurface);
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
        await using var client = new LaptopServiceClient();
        try
        {
            var screenResolutions = await client.GetScreenResolutionsAsync();
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
        await using var client = new LaptopServiceClient();
        try
        {
            return await client.GetAmountOfLaptopsByScreenResolutionAsync(screenResolution);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}