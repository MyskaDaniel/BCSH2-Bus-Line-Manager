using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BusLineManager.Core.Data;
using BusLineManager.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BusLineManager.Models;

public partial class BusOperatorPane : UserControl
{
    public BusOperatorPane(BusOperator busOperator)
    {
        InitializeComponent();
        var viewModel = new BusOperatorPaneViewModel();
        
        BusOperatorName.Text = busOperator.Name;
        AvailableBuses.Text = $"Available: {viewModel.AvailableBuses}";
        OnLineBuses.Text = $"On Lines: {viewModel.OnLineBuses}";
    }
    
    public BusOperatorPane GetBusOperatorPane => this;
}