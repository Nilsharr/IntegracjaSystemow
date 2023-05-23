using System.ServiceModel;
using SoapServer.Dto;

namespace SoapServer.Interfaces;

[ServiceContract]
public interface ILaptopService
{
    [OperationContract]
    [FaultContract(typeof(MissingProfileFault))]
    Task<IEnumerable<Laptop>> GetAllLaptops();

    [OperationContract]
    [FaultContract(typeof(MissingProfileFault))]
    Task<IEnumerable<string>> GetProducers();

    [OperationContract]
    [FaultContract(typeof(MissingProfileFault))]
    Task<int> GetAmountOfLaptopsByProducer(string producer);

    [OperationContract]
    [FaultContract(typeof(MissingProfileFault))]
    Task<IEnumerable<string>> GetScreenSurfaces();

    [OperationContract]
    [FaultContract(typeof(MissingProfileFault))]
    Task<IEnumerable<Laptop>> GetLaptopsByScreenSurface(string screenSurface);

    [OperationContract]
    [FaultContract(typeof(MissingProfileFault))]
    Task<IEnumerable<string>> GetScreenResolutions();

    [OperationContract]
    [FaultContract(typeof(MissingProfileFault))]
    Task<int> GetAmountOfLaptopsByScreenResolution(string screenResolution);
}