using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using BusLineManager.ViewModels;

namespace BusLineManager.Views;

public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
        var win = new MainWindowViewModel();
        
        var dlist = this.FindControl<StackPanel>("DopravceList") ?? throw new Exception("Could load Control DopravceList");

        var busOperators = win.GetAllBusOperators();

        foreach (var op in busOperators)
        {
            var dockPanel = CreateDockPanel();
            dlist.Children.Add(dockPanel);
        }
    }

    private void CloseApplication(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private DockPanel CreateDockPanel()
    {
        return new DockPanel
        {
            Classes = { "Dopravce" },
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };
    }
}