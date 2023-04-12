using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using IntegracjaSystemow.Models;
using ReactiveUI;
using IntegracjaSystemow.Utils;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace IntegracjaSystemow.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private ReactiveCommand<string, Unit> OpenFile { get; }
    private ReactiveCommand<string, Unit> SaveFile { get; }
    public Interaction<string, string[]?> ShowOpenFileDialog { get; }
    public Interaction<string, string?> ShowSaveFileDialog { get; }
    private ObservableCollection<Laptop> _laptops = new();

    public ObservableCollection<Laptop> Laptops
    {
        get => _laptops;
        set => this.RaiseAndSetIfChanged(ref _laptops, value);
    }

    public MainWindowViewModel()
    {
        OpenFile = ReactiveCommand.CreateFromTask<string>(ImportDataFromFile);
        SaveFile = ReactiveCommand.CreateFromTask<string>(ExportDataToFile);
        ShowOpenFileDialog = new Interaction<string, string[]?>();
        ShowSaveFileDialog = new Interaction<string, string?>();
    }

    public async Task ImportDataFromFile(string extension)
    {
        var filePaths = await ShowOpenFileDialog.Handle(extension);
        if (filePaths is not null)
        {
            foreach (var path in filePaths)
            {
                switch (extension)
                {
                    case "txt":
                        Laptops.AddRange(DataUtils.ReadTxtFile(path));
                        break;
                    case "xml":
                        Laptops.AddRange(DataUtils.ReadXmlFile(path));
                        break;
                }
            }
        }
    }

    public async Task ImportDataFromDb()
    {
        
    }

    public async Task ExportDataToDb()
    {
        
    }

    public async Task ExportDataToFile(string extension)
    {
        var filePath = await ShowSaveFileDialog.Handle(extension);
        if (filePath is not null)
        {
            switch (extension)
            {
                case "txt":
                    await DataUtils.SaveTxtFile(filePath, _laptops);
                    break;
                case "xml":
                    await DataUtils.SaveXmlFile(filePath, _laptops);
                    break;
            }
        }
    }

    public void ClearData()
    {
        Laptops = new();
    }
}
