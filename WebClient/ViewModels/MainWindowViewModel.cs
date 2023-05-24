using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using WebClient.Interfaces;
using WebClient.Services;
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

    private readonly IApiMethods _apiMethods;

    public MainWindowViewModel()
    {
        var useRest = true;
        if (useRest)
        {
            _apiMethods = new RestService();
        }
        else
        {
            _apiMethods = new SoapService();
        }

        RxApp.MainThreadScheduler.Schedule(LoadComboBoxes);
    }

    public async Task OnProducerSelectionChanged(object? sender)
    {
        var producer = (sender as ComboBox)?.SelectedItem?.ToString();
        AmountOfLaptopsByProducer = await _apiMethods.GetAmountOfLaptopsByProducer(producer!);
    }

    public async void OnScreenResolutionSelectionChanged(object? sender)
    {
        var screenResolution = (sender as ComboBox)?.SelectedItem?.ToString();
        AmountOfLaptopsByScreenResolution = await _apiMethods.GetAmountOfLaptopsByScreenResolution(screenResolution!);
    }

    public async void OnScreenSurfaceSelectionChanged(object? sender)
    {
        var screenSurface = (sender as ComboBox)?.SelectedItem?.ToString();

        Laptops = new ObservableCollection<Laptop>(await _apiMethods.GetLaptopsByScreenSurface(screenSurface!));
    }

    public async Task GetAll()
    {
        Laptops = new ObservableCollection<Laptop>(await _apiMethods.GetAllLaptops());
    }

    public void ClearData()
    {
        Laptops = new();
    }

    private async void LoadComboBoxes()
    {
        Producers = new ObservableCollection<string>(await _apiMethods.GetProducers());
        ScreenSurfaces = new ObservableCollection<string>(await _apiMethods.GetScreenSurfaces());
        ScreenResolutions = new ObservableCollection<string>(await _apiMethods.GetScreenResolutions());
    }
}