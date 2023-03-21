using System;
using System.Threading.Tasks;
using ReactiveUI;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using IntegracjaSystemow.ViewModels;

namespace IntegracjaSystemow.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(d => d(ViewModel!.ShowOpenFileDialog.RegisterHandler(ShowOpenFileDialog)));
        this.WhenActivated(d => d(ViewModel!.ShowSaveFileDialog.RegisterHandler(ShowSaveFileDialog)));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task ShowOpenFileDialog(InteractionContext<string, string[]?> interaction)
    {
        var extension = interaction.Input;
        var dialog = new OpenFileDialog();
        dialog.Filters!.Add(new FileDialogFilter {Name = extension, Extensions = {extension}});
        var fileNames = await dialog.ShowAsync(this);
        interaction.SetOutput(fileNames);
    }

    private async Task ShowSaveFileDialog(InteractionContext<string, string?> interaction)
    {
        var extension = interaction.Input;
        var name = extension switch
        {
            "txt" => "Text Document",
            "xml" => "XML Document",
            _ => throw new ArgumentOutOfRangeException(extension)
        };

        var saveFileDialog = new SaveFileDialog
        {
            Title = "Save File"
        };
        saveFileDialog.Filters!.Add(new FileDialogFilter {Name = name, Extensions = {extension}});
        saveFileDialog.InitialFileName = $"katalog.{extension}";
        saveFileDialog.DefaultExtension = extension;

        var path = await saveFileDialog.ShowAsync(this);
        interaction.SetOutput(path);
    }
}