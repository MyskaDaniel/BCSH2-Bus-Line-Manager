using Avalonia.Controls;
using BusLineManager.Models;
using BusLineManager.ViewModels;

namespace BusLineManager.Views.Controls;

public partial class LinePane : UserControl
{
    public LinePane(BusLine line)
    {
        InitializeComponent();

        DataContext = new LinePaneViewModel(line);

    }
}