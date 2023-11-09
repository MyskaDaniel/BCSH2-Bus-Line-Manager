using System.Collections.Generic;
using BusLineManager.Models;

namespace BusLineManager.Core.Service;

public class BusAvailabilityService
{
    private readonly Database.Database _database = new ();
    
    public long OperatorId { get; set; }

    public BusAvailabilityService(BusOperator busOperator)
    {
        OperatorId = busOperator.Id;
    }

    public List<Bus> AvailableBuses => _database.FindAllAvailableBusesForBusOperator(OperatorId);
    public List<Bus> OnlineBuses => _database.FindAllBusesOnLineForBusOperator(OperatorId);
    
}