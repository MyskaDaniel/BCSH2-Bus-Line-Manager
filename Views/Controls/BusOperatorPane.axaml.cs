using Avalonia.Controls;
using BusLineManager.Core.Data;
using BusLineManager.ViewModels;

namespace BusLineManager.Views.Controls;

public partial class BusOperatorPane : UserControl
{
    public readonly BusOperator BusOperator;
    
    public BusOperatorPane(BusOperator busOperator)
    {
        InitializeComponent();
        BusOperator = busOperator;
        var viewModel = new BusOperatorPaneViewModel(busOperator);
        
        BusOperatorName.Text = busOperator.Name;
        AvailableBuses.Text = $"Available: {viewModel.AvailableBuses}";
        OnLineBuses.Text = $"On Lines: {viewModel.OnLineBuses}";
    }
    
}