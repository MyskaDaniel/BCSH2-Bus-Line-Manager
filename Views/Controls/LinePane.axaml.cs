using Avalonia.Controls;
using BusLineManager.ViewModels;

namespace BusLineManager.Views.Controls;

public partial class LinePane : UserControl
{
    public LinePane()
    {
        InitializeComponent();
        var viewModel = new LinePaneViewModel();

        LineName.Text = "viewModel";
    }
}