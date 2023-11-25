using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using BusLineManager.Models;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class LinePaneViewModel :  ViewModelBase, IReactiveObject
{
    private string _busOperatorName;
    private string _startStation;
    private string _endStation;
    
    private readonly BusLine _busLine;
        
    public LinePaneViewModel(BusLine busLine)
    {
        _busLine = busLine;
        _busOperatorName = busLine.Name;
        _startStation = busLine.StartStation;
        _endStation = busLine.EndStation;
    }
    
    public async Task UpdateUi(CancellationToken cancellationToken)
    {
        try
        {
            await Task.Run(() =>
            {
                LineName = "lol";
            }, cancellationToken);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
        }
    }

    public string LineName  
    {
        get => _busOperatorName;
        set => this.RaiseAndSetIfChanged(ref _busOperatorName,  value);
    }

    public string StartStation 
    {
      get => $"Start Station: {_busLine.StartStation}";
      set => this.RaiseAndSetIfChanged(ref _startStation, value);
    }

    public string EndStation
    {
        get => $"Start Station: {_busLine.EndStation}";
        set => this.RaiseAndSetIfChanged(ref _endStation, value);
    }

    public BusLine BusLine => _busLine;

}