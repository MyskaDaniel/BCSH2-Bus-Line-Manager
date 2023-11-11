using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using BusLineManager.Models;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class LinePaneViewModel :  ViewModelBase, IReactiveObject
{
    private string _busOperatorName;
    public LinePaneViewModel(BusLine busLine)
    {
        _busOperatorName = busLine.Name;
        StartStation = $"Start Station: {busLine.StartStation}";
        EndStation = $"End Station: {busLine.EndStation}";
    }

    public async Task UpdateUi()
    {
        await Task.Run(() =>
        {
            LineName = "lol";
        });
    }

    public string LineName  {
        get => _busOperatorName;
        set => this.RaiseAndSetIfChanged(ref _busOperatorName,  value);
    }
    public string StartStation { get; }
    public string EndStation { get; }
    
}