namespace BusLineManager.Models;

public record BusLine(long Id, string Name, long BusOpearatorId, string StartStation, string EndStation);