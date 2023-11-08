using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BusLineManager.Core.Data;

namespace BusLineManager.Views.Controls;

public partial class BusPane : UserControl
{
    public BusPane(Bus bus)
    {
        InitializeComponent();
        BusSpz.Text = bus.Spz;
        BusCapacity.Text = bus.Capacity.ToString();
    }
}