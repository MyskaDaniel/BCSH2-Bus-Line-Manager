using BusLineManager.Models;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class EditLineViewModel :  ViewModelBase, IReactiveObject
{
    private readonly BusLine _busLine;
    public EditLineViewModel(LinePaneViewModel linePane)
    {
        _busLine = linePane.BusLine;
    }

    public string LineName => _busLine.Name;
    public string StartStation => _busLine.StartStation;
    public string EndStation => _busLine.EndStation;
}