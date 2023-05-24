using System;
using System.ComponentModel;
using WebClient.Dto;

namespace WebClient.Models;

public class Laptop : INotifyPropertyChanged
{
    public int Id { get; set; }
    private string _producer = default!;

    public string Producer
    {
        get => _producer;
        set
        {
            if (!string.Equals(_producer, value, StringComparison.OrdinalIgnoreCase))
            {
                _producer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
            }
        }
    }

    private string? _screenDiagonal;

    public string? ScreenDiagonal
    {
        get => _screenDiagonal;
        set
        {
            if (!string.Equals(_screenDiagonal, value, StringComparison.OrdinalIgnoreCase))
            {
                _screenDiagonal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScreenDiagonal)));
            }
        }
    }

    private string? _screenResolution;

    public string? ScreenResolution
    {
        get => _screenResolution;
        set
        {
            if (!string.Equals(_screenResolution, value, StringComparison.OrdinalIgnoreCase))
            {
                _screenResolution = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScreenResolution)));
            }
        }
    }

    private string? _screenSurface;

    public string? ScreenSurface
    {
        get => _screenSurface;
        set
        {
            if (!string.Equals(_screenSurface, value, StringComparison.OrdinalIgnoreCase))
            {
                _screenSurface = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScreenSurface)));
            }
        }
    }

    private string _isTouchScreen = default!;

    public string IsTouchScreen
    {
        get => _isTouchScreen;
        set
        {
            if (!string.Equals(_isTouchScreen, value, StringComparison.OrdinalIgnoreCase))
            {
                _isTouchScreen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTouchScreen)));
            }
        }
    }

    private string? _processor;

    public string? Processor
    {
        get => _processor;
        set
        {
            if (!string.Equals(_processor, value, StringComparison.OrdinalIgnoreCase))
            {
                _processor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Processor)));
            }
        }
    }

    private int? _physicalCores;

    public int? PhysicalCores
    {
        get => _physicalCores;
        set
        {
            if (_physicalCores != value)
            {
                _physicalCores = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PhysicalCores)));
            }
        }
    }

    private int? _clockSpeed;

    public int? ClockSpeed
    {
        get => _clockSpeed;
        set
        {
            if (_clockSpeed != value)
            {
                _clockSpeed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClockSpeed)));
            }
        }
    }

    private string? _memorySize;

    public string? MemorySize
    {
        get => _memorySize;
        set
        {
            if (!string.Equals(_memorySize, value, StringComparison.OrdinalIgnoreCase))
            {
                _memorySize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MemorySize)));
            }
        }
    }

    private string? _diskCapacity;

    public string? DiskCapacity
    {
        get => _diskCapacity;
        set
        {
            if (!string.Equals(_diskCapacity, value, StringComparison.OrdinalIgnoreCase))
            {
                _diskCapacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DiskCapacity)));
            }
        }
    }

    private string? _diskType;

    public string? DiskType
    {
        get => _diskType;
        set
        {
            if (!string.Equals(_diskType, value, StringComparison.OrdinalIgnoreCase))
            {
                _diskType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DiskType)));
            }
        }
    }

    private string? _graphicCard;

    public string? GraphicCard
    {
        get => _graphicCard;
        set
        {
            if (!string.Equals(_graphicCard, value, StringComparison.OrdinalIgnoreCase))
            {
                _graphicCard = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GraphicCard)));
            }
        }
    }

    private string? _graphicCardMemory;

    public string? GraphicCardMemory
    {
        get => _graphicCardMemory;
        set
        {
            if (!string.Equals(_graphicCardMemory, value, StringComparison.OrdinalIgnoreCase))
            {
                _graphicCardMemory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GraphicCardMemory)));
            }
        }
    }

    private string? _operatingSystem;

    public string? OperatingSystem
    {
        get => _operatingSystem;
        set
        {
            if (!string.Equals(_operatingSystem, value, StringComparison.OrdinalIgnoreCase))
            {
                _operatingSystem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OperatingSystem)));
            }
        }
    }

    private string? _physicalDriveType;

    public string? PhysicalDriveType
    {
        get => _physicalDriveType;
        set
        {
            if (!string.Equals(_physicalDriveType, value, StringComparison.OrdinalIgnoreCase))
            {
                _physicalDriveType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PhysicalDriveType)));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Laptop(ILaptopService.Laptop laptop)
    {
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

    public Laptop(LaptopDto laptop)
    {
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

    public override string ToString()
    {
        return
            $"{Producer};{ScreenDiagonal};{ScreenResolution};{ScreenSurface};{IsTouchScreen};{Processor};{PhysicalCores};{ClockSpeed};{MemorySize};{DiskCapacity};{DiskType};{GraphicCard};{GraphicCardMemory};{OperatingSystem};{PhysicalDriveType}";
    }

    // cant overload equals because data grid wont work xd
    public bool EqualsValue(Laptop other)
    {
        return string.Equals(Producer, other.Producer, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(ScreenDiagonal, other.ScreenDiagonal, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(ScreenResolution, other.ScreenResolution, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(ScreenSurface, other.ScreenSurface, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(IsTouchScreen, other.IsTouchScreen, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(Processor, other.Processor, StringComparison.OrdinalIgnoreCase) &&
               PhysicalCores == other.PhysicalCores &&
               ClockSpeed == other.ClockSpeed &&
               string.Equals(MemorySize, other.MemorySize, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(DiskCapacity, other.DiskCapacity, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(DiskType, other.DiskType, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(GraphicCard, other.GraphicCard, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(GraphicCardMemory, other.GraphicCardMemory, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(OperatingSystem, other.OperatingSystem, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(PhysicalDriveType, other.PhysicalDriveType, StringComparison.OrdinalIgnoreCase);
    }
}