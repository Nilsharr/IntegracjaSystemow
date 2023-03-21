using System.Xml.Linq;

namespace IntegracjaSystemow.Models;

public class Laptop
{
    public string Producer { get; init; } = default!;
    public string? ScreenDiagonal { get; init; }
    public string? ScreenResolution { get; init; }
    public string? ScreenSurface { get; init; }
    public string IsTouchScreen { get; init; } = default!;
    public string? Processor { get; init; }
    public int? PhysicalCores { get; init; }
    public int? ClockSpeed { get; init; }
    public string? MemorySize { get; init; }
    public string? DiskCapacity { get; init; }
    public string? DiskType { get; init; }
    public string? GraphicCard { get; init; }
    public string? GraphicCardMemory { get; init; }
    public string? OperatingSystem { get; init; }
    public string? PhysicalDriveType { get; init; }

    public override string ToString()
    {
        return
            $"{Producer};{ScreenDiagonal};{ScreenResolution};{ScreenSurface};{IsTouchScreen};{Processor};{PhysicalCores};{ClockSpeed};{MemorySize};{DiskCapacity};{DiskType};{GraphicCard};{GraphicCardMemory};{OperatingSystem};{PhysicalDriveType}";
    }

    public XElement ToXml(int id)
    {
        return new XElement("laptop",
            new XAttribute("id", id),
            new XElement("manufacturer", Producer),
            new XElement("screen",
                new XAttribute("touch", IsTouchScreen),
                new XElement("size", ScreenDiagonal),
                new XElement("resolution", ScreenResolution),
                new XElement("type", ScreenSurface)),
            new XElement("processor",
                new XElement("name", Processor),
                new XElement("physical_cores", PhysicalCores),
                new XElement("clock_speed", ClockSpeed)),
            new XElement("ram", MemorySize),
            new XElement("disc",
                new XAttribute("type", DiskType ?? string.Empty),
                new XElement("storage", DiskCapacity)),
            new XElement("graphic_card",
                new XElement("name", GraphicCard),
                new XElement("memory", GraphicCardMemory)),
            new XElement("os", OperatingSystem),
            new XElement("disc_reader", PhysicalDriveType ?? "brak"));
    }
}