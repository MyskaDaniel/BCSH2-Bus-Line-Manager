using ReactiveUI;

namespace BusLineManager.ViewModels;

public class BusOperatorPaneViewModel :  ViewModelBase, IReactiveObject
{
    public int AvailableBuses => 10;

    public int OnLineBuses => 12;
}