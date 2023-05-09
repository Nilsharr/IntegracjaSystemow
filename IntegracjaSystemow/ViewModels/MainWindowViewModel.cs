using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                        var txtLaptops = DataUtils.ReadTxtFile(path);
                        SetDuplicates(txtLaptops);
                        Laptops.AddRange(txtLaptops);
                        break;
                    case "xml":
                        var xmlLaptops = DataUtils.ReadXmlFile(path);
                        SetDuplicates(xmlLaptops);
                        Laptops.AddRange(xmlLaptops);
                        break;
                }
            }
        }
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

    public async Task ImportDataFromDb()
    {
        var laptops = await DataUtils.ReadFromDatabase();
        SetDuplicates(laptops);
        Laptops.AddRange(laptops);
    }

    public async Task ExportDataToDb()
    {
        await DataUtils.SaveToDatabase(Laptops);
    }

    public void ClearData()
    {
        Laptops = new();
    }

    private void SetDuplicates(ICollection<Laptop> newLaptops)
    {
        var duplicates = 0;

        foreach (var newLaptop in newLaptops)
        {
            if (Laptops.Any(laptop => laptop.EqualsValue(newLaptop)))
            {
                duplicates++;
                newLaptop.RowStatus = RowStatus.Duplicated;
            }
            else
            {
                newLaptop.RowStatus = RowStatus.NotEdited;
            }
        }

        ShowAddedRecordsDialog(duplicates, newLaptops.Count - duplicates);
    }

    private static void ShowAddedRecordsDialog(int duplicates, int newRecords)
    {
        var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxStandardWindow("New records",
                $"Added {duplicates + newRecords} rows ({duplicates} duplicates, {newRecords} new records). ");
        messageBoxStandardWindow.Show();
    }
}