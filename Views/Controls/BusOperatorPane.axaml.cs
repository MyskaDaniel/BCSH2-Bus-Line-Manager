using Avalonia.Controls;
using BusLineManager.Core.Data;
using BusLineManager.ViewModels;

namespace BusLineManager.Views.Controls;

public partial class BusOperatorPane : UserControl
{
    public BusOperatorPane(BusOperator busOperator)
    {
        InitializeComponent();
        DataContext = new BusOperatorPaneViewModel(busOperator);
    }
    
}