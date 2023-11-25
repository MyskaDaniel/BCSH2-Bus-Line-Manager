using System;
using BusLineManager.Core.Service;
using BusLineManager.Models;
using BusLineManager.Views.Controls;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class BusOperatorPaneViewModel :  ViewModelBase, IReactiveObject
{
    private readonly BusOperatorService _service;

    public BusOperatorPaneViewModel(BusOperator busOperator)
    {
        _service = new BusOperatorService(busOperator);
        
        BusOperatorName = busOperator.Name;
        OperatorIco = busOperator.Ico;
    }

    public string AvailableBuses => _service.AvailableBuses.Count.ToString();

    public string OnLineBuses => _service.OnlineBuses.Count.ToString();

    public string BusOperatorName { get; }
    public string OperatorIco { get; }

    public void DeleteBusOperator()
    {
        _service.DeleteBusOperator();
    }

    public void EditBusOperator()
    {
        
    }
    
    
}