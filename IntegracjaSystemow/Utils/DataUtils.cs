using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using IntegracjaSystemow.Models;
using AutoMapper;
using IntegracjaSystemow.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace IntegracjaSystemow.Utils;

public static class DataUtils
{
    private static readonly IMapper Mapper;

    static DataUtils()
    {
        Mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Database.Entities.Laptop, Laptop>();
            cfg.CreateMap<Laptop, Database.Entities.Laptop>();
        }));
    }

    public static IEnumerable<ProducerCount> CountProducers(IEnumerable<Laptop> laptops)
    {
        if (laptops is null)
        {
            throw new ArgumentNullException(nameof(laptops));
        }

        return laptops.GroupBy(laptop => laptop.Producer.ToLower()).Select(group => new ProducerCount
            {Producer = group.Key.Capitalize(), Count = group.Count()});
    }

    public static IList<Laptop> ReadTxtFile(string filePath, string delimiter = ";")
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Text file not found at path: {filePath}");
        }

        var laptops = new List<Laptop>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var line in File.ReadLines(filePath))
        {
            var data = line.Split(delimiter);

            laptops.Add(new Laptop
            {
                Producer = data[0],
                ScreenDiagonal = data[1],
                ScreenResolution = data[2],
                ScreenSurface = data[3],
                IsTouchScreen = data[4],
                Processor = data[5],
                PhysicalCores = ParseNullableInt(data[6]),
                ClockSpeed = ParseNullableInt(data[7]),
                MemorySize = data[8],
                DiskCapacity = data[9],
                DiskType = data[10],
                GraphicCard = data[11],
                GraphicCardMemory = data[12],
                OperatingSystem = data[13],
                PhysicalDriveType = data[14]
            });
        }

        return laptops;
    }

    public static async Task SaveTxtFile(string filePath, IEnumerable<Laptop> data)
    {
        await using StreamWriter writer = new(filePath, append: true);
        foreach (var laptop in data)
        {
            await writer.WriteLineAsync(laptop.ToString());
        }
    }

    public static IList<Laptop> ReadXmlFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Xml file not found at path: {filePath}");
        }

        var xmlLaptops = XDocument.Load(filePath);

        var laptops = (xmlLaptops.Root?.Elements("laptop") ?? Array.Empty<XElement>()).Select(laptopElem => new Laptop
        {
            Producer = laptopElem.Element("manufacturer")?.Value ?? string.Empty,
            ScreenDiagonal = laptopElem.Element("screen")?.Element("size")?.Value,
            ScreenResolution = laptopElem.Element("screen")?.Element("resolution")?.Value,
            ScreenSurface = laptopElem.Element("screen")?.Element("type")?.Value,
            IsTouchScreen = laptopElem.Element("screen")?.Attribute("touch")?.Value ?? "no",
            Processor = laptopElem.Element("processor")?.Element("name")?.Value,
            PhysicalCores = ParseNullableInt(laptopElem.Element("processor")?.Element("physical_cores")?.Value),
            ClockSpeed = ParseNullableInt(laptopElem.Element("processor")?.Element("clock_speed")?.Value),
            MemorySize = laptopElem.Element("ram")?.Value,
            DiskCapacity = laptopElem.Element("disc")?.Element("storage")?.Value,
            DiskType = laptopElem.Element("disc")?.Attribute("type")?.Value,
            GraphicCard = laptopElem.Element("graphic_card")?.Element("name")?.Value,
            GraphicCardMemory = laptopElem.Element("graphic_card")?.Element("memory")?.Value,
            OperatingSystem = laptopElem.Element("os")?.Value,
            PhysicalDriveType = laptopElem.Element("disc_reader")?.Value
        }).ToList();

        return laptops;
    }

    public static async Task SaveXmlFile(string filePath, IEnumerable<Laptop> data)
    {
        var laptopsXml = new XElement("laptops", new XAttribute("moddate", DateTime.Now.ToString("yyyy-MM-ddTHH:mm")),
            data.Select((laptop, i) => laptop.ToXml(i + 1)));
        if (File.Exists(filePath))
        {
            await File.AppendAllTextAsync(filePath, laptopsXml.ToString());
        }
        else
        {
            await File.WriteAllTextAsync(filePath, laptopsXml.ToString());
        }
    }

    public static async Task<IList<Laptop>> ReadFromDatabase()
    {
        await using var dbContext = new LaptopContext();
        return Mapper.Map<IList<Laptop>>(await dbContext.Laptops.ToListAsync());
    }

    public static async Task SaveToDatabase(IEnumerable<Laptop> data)
    {
        await using var dbContext = new LaptopContext();

        foreach (var laptop in data.Where(x => x.RowStatus != RowStatus.Duplicated))
        {
            // kekw
            if (!dbContext.Laptops.Any(l => l.Producer == laptop.Producer
                                            && l.ScreenDiagonal == laptop.ScreenDiagonal
                                            && l.ScreenResolution == laptop.ScreenResolution
                                            && l.ScreenSurface == laptop.ScreenSurface
                                            && l.IsTouchScreen == laptop.IsTouchScreen
                                            && l.Processor == laptop.Processor
                                            && l.PhysicalCores == laptop.PhysicalCores
                                            && l.ClockSpeed == laptop.ClockSpeed
                                            && l.MemorySize == laptop.MemorySize
                                            && l.DiskCapacity == laptop.DiskCapacity
                                            && l.DiskType == laptop.DiskType
                                            && l.GraphicCard == laptop.GraphicCard
                                            && l.GraphicCardMemory == laptop.GraphicCardMemory
                                            && l.OperatingSystem == laptop.OperatingSystem
                                            && l.PhysicalDriveType == laptop.PhysicalDriveType))
            {
                dbContext.Laptops.Add(Mapper.Map<Database.Entities.Laptop>(laptop));
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private static int? ParseNullableInt(string? value) => int.TryParse(value, out var result) ? result : default(int?);
}