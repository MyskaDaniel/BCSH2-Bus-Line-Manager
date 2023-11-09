using BusLineManager.Models;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class LinePaneViewModel :  ViewModelBase, IReactiveObject
{

    public LinePaneViewModel(BusLine busLine)
    {
        LineName = busLine.Name;
        StartStation = $"Start Station: {busLine.StartStation}";
        EndStation = $"End Station: {busLine.EndStation}";
    }

    public string LineName { get; }
    public string StartStation { get; }
    public string EndStation { get; }
}