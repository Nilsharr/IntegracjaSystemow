using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using WebClient.Dto;
using WebClient.Interfaces;
using WebClient.Models;

namespace WebClient.Services;

public class RestService : IApiMethods, IDisposable
{
    private readonly RestClient _client;

    public RestService()
    {
        var options = new RestClientOptions("http://localhost:5172/api/laptops");
        _client = new RestClient(options);
    }

    public async Task<IEnumerable<Laptop>> GetAllLaptops()
    {
        try
        {
            var request = new RestRequest("");
            var laptops = await _client.GetAsync<IEnumerable<LaptopDto>>(request);
            return (laptops ?? Array.Empty<LaptopDto>()).Select(item => new Laptop(item)).ToList();
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
            var request = new RestRequest("producers");
            var producers = await _client.GetAsync<IEnumerable<string>>(request);
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
            var request = new RestRequest("producers/amount").AddParameter("producer", producer);
            return await _client.GetAsync<int>(request);
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
            var request = new RestRequest("screen-surface");
            var producers = await _client.GetAsync<IEnumerable<string>>(request);
            return producers ?? Array.Empty<string>();
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
            var request = new RestRequest("").AddParameter("screenSurface", screenSurface);
            var laptops = await _client.GetAsync<IEnumerable<LaptopDto>>(request);
            return (laptops ?? Array.Empty<LaptopDto>()).Select(item => new Laptop(item)).ToList();
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
            var request = new RestRequest("screen-resolution");
            var screenResolutions = await _client.GetAsync<IEnumerable<string>>(request);
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
            var request =
                new RestRequest("screen-resolution/amount").AddParameter("screenResolution", screenResolution);
            return await _client.GetAsync<int>(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task<Laptop?> AddLaptop(Laptop laptop)
    {
        try
        {
            var request = new RestRequest("").AddJsonBody(laptop);
            var added = await _client.PostAsync<LaptopDto>(request);
            return new Laptop(added!);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task<Laptop?> UpdateLaptop(int id, Laptop laptop)
    {
        try
        {
            var request = new RestRequest($"{id}").AddJsonBody(laptop);
            var updated = await _client.PutAsync<LaptopDto>(request);
            return new Laptop(updated!);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task DeleteLaptop(int id)
    {
        try
        {
            var request = new RestRequest($"{id}");
            await _client.DeleteAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}