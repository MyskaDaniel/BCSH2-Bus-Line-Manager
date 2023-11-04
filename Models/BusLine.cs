namespace BusLineManager.Core.Data;

public record BusLine(int? Id, string Name, int BusOpearatorId, string StartStation, string EndStation);