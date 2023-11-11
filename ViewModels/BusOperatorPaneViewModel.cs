using BusLineManager.Core.Service;
using BusLineManager.Models;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class BusOperatorPaneViewModel :  ViewModelBase, IReactiveObject
{
    private readonly BusAvailabilityService _service;

    public BusOperatorPaneViewModel(BusOperator busOperator)
    {
        _service = new BusAvailabilityService(busOperator);
        
        BusOperatorName = busOperator.Name;
        OperatorIco = busOperator.Ico;
    }

    public string AvailableBuses => _service.AvailableBuses.Count.ToString();

    public string OnLineBuses => _service.OnlineBuses.Count.ToString();

    public string BusOperatorName { get; }
    public string OperatorIco { get; }
    
    
}