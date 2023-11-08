namespace BusLineManager.Core.Data;

public record BusLine(long Id, string Name, long BusOpearatorId, string StartStation, string EndStation);