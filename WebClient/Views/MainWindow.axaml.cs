using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace WebClient.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
#if DEBUG
        this.AttachDevTools();
#endif
        InitializeComponent();
    }
}