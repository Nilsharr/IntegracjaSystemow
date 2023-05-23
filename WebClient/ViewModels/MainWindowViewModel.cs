using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using WebClient.ILaptopService;
using Laptop = WebClient.Models.Laptop;

namespace WebClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ObservableCollection<Laptop> _laptops = new();

    public ObservableCollection<Laptop> Laptops
    {
        get => _laptops;
        set => this.RaiseAndSetIfChanged(ref _laptops, value);
    }

    private ObservableCollection<string> _producers = new();

    public ObservableCollection<string> Producers
    {
        get => _producers;
        set => this.RaiseAndSetIfChanged(ref _producers, value);
    }

    private ObservableCollection<string> _screenResolutions = new();

    public ObservableCollection<string> ScreenResolutions
    {
        get => _screenResolutions;
        set => this.RaiseAndSetIfChanged(ref _screenResolutions, value);
    }

    private ObservableCollection<string> _screenSurfaces = new();

    public ObservableCollection<string> ScreenSurfaces
    {
        get => _screenSurfaces;
        set => this.RaiseAndSetIfChanged(ref _screenSurfaces, value);
    }

    private int? _amountOfLaptopsByProducer;

    public int? AmountOfLaptopsByProducer
    {
        get => _amountOfLaptopsByProducer;
        set => this.RaiseAndSetIfChanged(ref _amountOfLaptopsByProducer, value);
    }

    private int? _amountOfLaptopsByScreenResolution;

    public int? AmountOfLaptopsByScreenResolution
    {
        get => _amountOfLaptopsByScreenResolution;
        set => this.RaiseAndSetIfChanged(ref _amountOfLaptopsByScreenResolution, value);
    }

    public MainWindowViewModel()
    {
        RxApp.MainThreadScheduler.Schedule(LoadComboBoxes);
    }

    public async Task OnProducerSelectionChanged(object? sender)
    {
        var producer = (sender as ComboBox)?.SelectedItem?.ToString();

        await using var client = new LaptopServiceClient();
        try
        {
            var amountOfLaptops = await client.GetAmountOfLaptopsByProducerAsync(producer);
            AmountOfLaptopsByProducer = amountOfLaptops;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async void OnScreenResolutionSelectionChanged(object? sender)
    {
        var screenResolution = (sender as ComboBox)?.SelectedItem?.ToString();

        await using var client = new LaptopServiceClient();
        try
        {
            var amountOfLaptops = await client.GetAmountOfLaptopsByScreenResolutionAsync(screenResolution);
            AmountOfLaptopsByScreenResolution = amountOfLaptops;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async void OnScreenSurfaceSelectionChanged(object? sender)
    {
        var screenSurface = (sender as ComboBox)?.SelectedItem?.ToString();

        await using var client = new LaptopServiceClient();
        try
        {
            var laptops = await client.GetLaptopsByScreenSurfaceAsync(screenSurface);
            if (laptops is not null)
            {
                Laptops = new ObservableCollection<Laptop>(laptops.Select(item => new Laptop(item)));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async Task GetAll()
    {
        await using var client = new LaptopServiceClient();
        try
        {
            var laptops = await client.GetAllLaptopsAsync();
            if (laptops is not null)
            {
                Laptops.AddRange(laptops.Select(item => new Laptop(item)).ToList());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public void ClearData()
    {
        Laptops = new();
    }

    private async void LoadComboBoxes()
    {
        await SetProducers();
        await SetScreenSurfaces();
        await SetScreenResolutions();
    }

    private async Task SetProducers()
    {
        await using var client = new LaptopServiceClient();
        try
        {
            var producers = await client.GetProducersAsync();
            if (producers is not null)
            {
                Producers = new ObservableCollection<string>(producers);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task SetScreenSurfaces()
    {
        await using var client = new LaptopServiceClient();
        try
        {
            var screenSurfaces = await client.GetScreenSurfacesAsync();
            if (screenSurfaces is not null)
            {
                ScreenSurfaces = new ObservableCollection<string>(screenSurfaces);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task SetScreenResolutions()
    {
        await using var client = new LaptopServiceClient();
        try
        {
            var screenResolutions = await client.GetScreenResolutionsAsync();
            if (screenResolutions is not null)
            {
                ScreenResolutions = new ObservableCollection<string>(screenResolutions);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}