using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BusLineManager.Core.Data;
using BusLineManager.ViewModels;

namespace BusLineManager.Views.Controls;

public partial class BusPane : UserControl
{
    public BusPane(Bus bus)
    {
        InitializeComponent();
        DataContext = new BusPaneViewModel(bus);
    }
}