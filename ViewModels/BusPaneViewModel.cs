using ReactiveUI;

namespace BusLineManager.ViewModels;

public class BusPaneViewModel :  ViewModelBase, IReactiveObject
{
    public BusPaneViewModel()
    {
    }

    public string Spz => "TEST";
    public string Capacity => "100";
    public string BusOperatorName => "Test_Name";
}