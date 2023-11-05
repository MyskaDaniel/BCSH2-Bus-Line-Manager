using ReactiveUI;

namespace BusLineManager.ViewModels;

public class AlertViewModel :  ViewModelBase, IReactiveObject
{
    public AlertViewModel(string message)
    {
        Message = message;
    }

    public string Message { get; }
}