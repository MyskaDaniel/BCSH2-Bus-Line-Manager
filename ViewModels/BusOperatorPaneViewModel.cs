using BusLineManager.Core.Data;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class BusOperatorPaneViewModel :  ViewModelBase, IReactiveObject
{
    private readonly BusOperator _busOperator;

    public BusOperatorPaneViewModel(BusOperator busOperator)
    {
        _busOperator = busOperator;
    }

    public int AvailableBuses => 10;

    public int OnLineBuses => 12;

    public BusOperator BusOperator => _busOperator;
}