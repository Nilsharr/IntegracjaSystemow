using Microsoft.Extensions.DependencyInjection.Extensions;
using SoapCore;
using SoapServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.TryAddSingleton<LaptopService>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.UseSoapEndpoint<LaptopService>(path: "/LaptopService.asmx",
    encoder: new SoapEncoderOptions(), serializer: SoapSerializer.DataContractSerializer, caseInsensitivePath: true));

app.Run();