using System.Collections.ObjectModel;
using System.Reactive.Linq;
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

    private Laptop? _selectedLaptop;

    public Laptop? SelectedLaptop
    {
        get => _selectedLaptop;
        set => this.RaiseAndSetIfChanged(ref _selectedLaptop, value);
    }

    private readonly ObservableAsPropertyHelper<bool> _isLaptopSelected;
    public bool IsLaptopSelected => _isLaptopSelected.Value;

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

    public bool UseRestApi { get; set; } = false;

    private readonly IApiMethods _apiMethods;

    public MainWindowViewModel()
    {
        _isLaptopSelected = this.WhenAnyValue(x => x.SelectedLaptop).Select(x => x is not null)
            .ToProperty(this, x => x.IsLaptopSelected);

        if (UseRestApi)
        {
            _apiMethods = new RestService();
        }
        else
        {
            _apiMethods = new SoapService();
        }
    }

    public async Task OnProducerTapped()
    {
        Producers = new ObservableCollection<string>(await _apiMethods.GetProducers());
    }

    public async Task OnScreenResolutionTapped()
    {
        ScreenResolutions = new ObservableCollection<string>(await _apiMethods.GetScreenResolutions());
    }

    public async Task OnScreenSurfaceTapped()
    {
        ScreenSurfaces = new ObservableCollection<string>(await _apiMethods.GetScreenSurfaces());
    }

    public async Task OnProducerSelectionChanged(object? sender)
    {
        var producer = (sender as ComboBox)?.SelectedItem?.ToString();
        AmountOfLaptopsByProducer = await _apiMethods.GetAmountOfLaptopsByProducer(producer!);
    }

    public async Task OnScreenResolutionSelectionChanged(object? sender)
    {
        var screenResolution = (sender as ComboBox)?.SelectedItem?.ToString();
        AmountOfLaptopsByScreenResolution = await _apiMethods.GetAmountOfLaptopsByScreenResolution(screenResolution!);
    }

    public async Task OnScreenSurfaceSelectionChanged(object? sender)
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

    public void AddRow()
    {
        Laptops.Add(new Laptop {Producer = "Producent"});
    }

    public async Task AddLaptop()
    {
        if (SelectedLaptop is not null && SelectedLaptop.Id == 0)
        {
            await _apiMethods.AddLaptop(SelectedLaptop);
        }

        await GetAll();
    }

    public async Task UpdateLaptop()
    {
        if (SelectedLaptop is not null && SelectedLaptop.Id != 0)
        {
            await _apiMethods.UpdateLaptop(SelectedLaptop.Id, SelectedLaptop);
        }

        await GetAll();
    }

    public async Task DeleteLaptop()
    {
        if (SelectedLaptop is not null && SelectedLaptop.Id != 0)
        {
            await _apiMethods.DeleteLaptop(SelectedLaptop.Id);
        }

        await GetAll();
    }

    public void DeselectLaptop()
    {
        SelectedLaptop = null;
    }
}