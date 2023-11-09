namespace BusLineManager.Models;

public record Bus(long Id, string Spz, long BusOperatorId,long? LineId ,int Capacity);