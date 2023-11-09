using BusLineManager.Core.Data;
using BusLineManager.Core.Database;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class BusOperatorPaneViewModel :  ViewModelBase, IReactiveObject
{
    private readonly Database _database = new();

    public BusOperatorPaneViewModel(BusOperator busOperator)
    {
        BusOperatorName = busOperator.Name;
        AvailableBuses = "10";
        OnLineBuses = "12";
        OperatorIco = busOperator.Ico;
    }

    public string AvailableBuses { get; }

    public string OnLineBuses { get; }

    public string BusOperatorName { get; }
    public string OperatorIco { get; }
}