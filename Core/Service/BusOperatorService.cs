using System;
using System.Collections.Generic;
using BusLineManager.Models;

namespace BusLineManager.Core.Service;

public class BusOperatorService
{
    private readonly Database.Database _database = new ();
    private BusOperator _busOperator;
    
    public long OperatorId { get; set; }

    public BusOperatorService(BusOperator busOperator)
    {
        _busOperator = busOperator;
        OperatorId = busOperator.Id;
    }

    public List<Bus> AvailableBuses => _database.FindAllAvailableBusesForBusOperator(OperatorId);
    public List<Bus> OnlineBuses => _database.FindAllBusesOnLineForBusOperator(OperatorId);

    public void DeleteBusOperator()
    {
       var result =  _database.DeleteBusOperator(_busOperator);
       if (result > 1)
       {
           Console.WriteLine("Vic jak jeden bus operator byl smazan");
       }
    }
    
}