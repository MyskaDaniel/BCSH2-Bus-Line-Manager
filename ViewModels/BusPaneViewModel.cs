using System.Threading.Tasks;
using BusLineManager.Core.Data;
using BusLineManager.Core.Database;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class BusPaneViewModel :  ViewModelBase, IReactiveObject
{
    private readonly Database _database = new();
    public BusPaneViewModel(Bus bus)
    {
        Spz = bus.Spz;
        Capacity = bus.Capacity.ToString();
        BusOperatorName = _database.GetBusOperatorNameById(bus.BusOperatorId);
    }

    public string Spz { get; }
    public string Capacity { get; }
    public string BusOperatorName { get;  }
}