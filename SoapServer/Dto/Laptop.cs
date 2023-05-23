using System.Runtime.Serialization;

namespace SoapServer.Dto;

[DataContract]
public class Laptop
{
    [DataMember] public int Id { get; set; }
    [DataMember] public string Producer { get; init; } = default!;
    [DataMember] public string? ScreenDiagonal { get; init; }
    [DataMember] public string? ScreenResolution { get; init; }
    [DataMember] public string? ScreenSurface { get; init; }
    [DataMember] public string IsTouchScreen { get; init; } = default!;
    [DataMember] public string? Processor { get; init; }
    [DataMember] public int? PhysicalCores { get; init; }
    [DataMember] public int? ClockSpeed { get; init; }
    [DataMember] public string? MemorySize { get; init; }
    [DataMember] public string? DiskCapacity { get; init; }
    [DataMember] public string? DiskType { get; init; }
    [DataMember] public string? GraphicCard { get; init; }
    [DataMember] public string? GraphicCardMemory { get; init; }
    [DataMember] public string? OperatingSystem { get; init; }
    [DataMember] public string? PhysicalDriveType { get; init; }

    public Laptop()
    {
    }

    public Laptop(Database.Entities.Laptop laptop)
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