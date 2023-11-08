using Avalonia.Controls;
using BusLineManager.Core.Data;
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