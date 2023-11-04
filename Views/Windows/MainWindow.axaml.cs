using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using BusLineManager.Models;
using BusLineManager.ViewModels;

namespace BusLineManager.Views;

public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
        var win = new MainWindowViewModel();
        
        var listBox = this.FindControl<ListBox>("BusOperatorListBox") ?? throw new Exception("Could load Control DopravceList");

        var busOperators = win.GetAllBusOperators();

        foreach (var op in busOperators)
        {
            listBox.Items.Add(new BusOperatorPane(op));
            //dlist.Children.Add(new BusOperatorPane(op));
        }
    }

    private void CloseApplication(object? sender, RoutedEventArgs e) => Close();
}