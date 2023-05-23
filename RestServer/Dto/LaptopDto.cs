namespace RestServer.Dto;

public class LaptopDto
{
    public int Id { get; set; }
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

    public LaptopDto()
    {
    }

    public LaptopDto(Database.Entities.Laptop laptop)
    {
        Id = laptop.Id;
        Producer = laptop.Producer;
        ScreenDiagonal = laptop.ScreenDiagonal;
        ScreenResolution = laptop.ScreenResolution;
        ScreenSurface = laptop.ScreenSurface;
        IsTouchScreen = laptop.IsTouchScreen;
        Processor = laptop.Processor;
        PhysicalCores = laptop.PhysicalCores;
        ClockSpeed = laptop.ClockSpeed;
        MemorySize = laptop.MemorySize;
        DiskCapacity = laptop.DiskCapacity;
        DiskType = laptop.DiskType;
        GraphicCard = laptop.GraphicCard;
        GraphicCardMemory = laptop.GraphicCardMemory;
        OperatingSystem = laptop.OperatingSystem;
        PhysicalDriveType = laptop.PhysicalDriveType;
    }
}