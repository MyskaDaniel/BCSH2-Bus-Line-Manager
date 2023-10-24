using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace BusLineManager.Views;

public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
        
        var dlist = this.FindControl<StackPanel>("DopravceList");

        for (int i = 0; i < 10; i++)
        {
            var dockPanel = CreateDockPanel(dlist.Width);
            dlist.Children.Add(dockPanel);
        }
    }

    private void CloseApplication(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private DockPanel CreateDockPanel(double width)
    {
        return new DockPanel
        {
            Classes = { "Dopravce" },
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };
    }
}