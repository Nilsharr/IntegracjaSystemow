using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestServer.Database.Entities;

public class Laptop : IEntityTypeConfiguration<Laptop>
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

    void IEntityTypeConfiguration<Laptop>.Configure(EntityTypeBuilder<Laptop> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Producer).IsRequired();
        builder.Property(x => x.IsTouchScreen).IsRequired();
    }

    public Laptop()
    {
    }

    public Laptop(Dto.LaptopDto laptop)
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