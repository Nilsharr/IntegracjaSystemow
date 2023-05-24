using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Interfaces;

public interface IApiMethods
{
    Task<IEnumerable<Laptop>> GetAllLaptops();
    Task<IEnumerable<string>> GetProducers();
    Task<int?> GetAmountOfLaptopsByProducer(string producer);
    Task<IEnumerable<string>> GetScreenSurfaces();
    Task<IEnumerable<Laptop>> GetLaptopsByScreenSurface(string screenSurface);
    Task<IEnumerable<string>> GetScreenResolutions();
    Task<int?> GetAmountOfLaptopsByScreenResolution(string screenResolution);
}