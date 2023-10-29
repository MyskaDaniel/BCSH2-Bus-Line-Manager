using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using BusLineManager.Core.Data;

namespace BusLineManager.Core.Database;

public class Database
{
    private const string ConnectionString = "Data Source=buslinemanager.db;Version=3;"; //TODO readonly

    public static SQLiteConnection GetConnection() => new(ConnectionString);
    
    public void InsertBusOperator(BusOperator busOperator)
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        var insertQuery = "INSERT INTO BusOperators (Name, ICO) VALUES (@Name, @ICO)";
        using var command = new SQLiteCommand(insertQuery, connection);

        command.Parameters.AddWithValue("@Name", busOperator.Name);
        command.Parameters.AddWithValue("@Ico", busOperator.Ico);
        command.ExecuteNonQuery();

        connection.Close();
    }

    public void InsertLine(BusLine busLine)
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        var insertQuery = @"INSERT INTO Lines (Name, BusOperatorID, ListOfStations, StartStation, EndStation) 
                            VALUES (@Name, @BusOperatorID, @ListOfStations, @StartStation, @EndStation)";
    
        using var command = new SQLiteCommand(insertQuery, connection);

        command.Parameters.AddWithValue("@Name", busLine.Name);
        command.Parameters.AddWithValue("@BusOperatorID", busLine.BusOpearatorId);
        command.Parameters.AddWithValue("@StartStation", busLine.StartStation);
        command.Parameters.AddWithValue("@EndStation", busLine.EndStation);
    
        command.ExecuteNonQuery();

        connection.Close();
    }
    
    public void InsertBus(Bus bus)
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        string insertQuery = "INSERT INTO Buses (SPZ, BusOperatorID, Capacity) VALUES (@SPZ, @BusOperatorID, @Capacity)";
    
        using var command = new SQLiteCommand(insertQuery, connection);

        command.Parameters.AddWithValue("@SPZ", bus.Spz);
        command.Parameters.AddWithValue("@BusOperatorID", bus.BusOperatorId);
        command.Parameters.AddWithValue("@Capacity", bus.Capacity);
    
        command.ExecuteNonQuery();

        connection.Close();
    }

    public List<BusOperator> GetAllBusOperators()
    {
        var busOperators = new List<BusOperator>();

        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        var selectQuery = "SELECT * FROM BusOperators";

        using var command = new SQLiteCommand(selectQuery, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            busOperators.Add(new BusOperator(
                Id: reader.GetInt32(0),
                Name: reader.GetString(1),
                Ico: reader.GetString(2)
            ));
        }

        connection.Close();

        return busOperators;
    }
    
    public List<BusLine> GetAllLines()
    {
        var busLines = new List<BusLine>();

        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        var selectQuery = "SELECT * FROM Lines";

        using var command = new SQLiteCommand(selectQuery, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            busLines.Add(new BusLine(
                Id: reader.GetInt32(0),
                Name: reader.GetString(1),
                BusOpearatorId: reader.GetInt32(2),
                StartStation: reader.GetString(4),
                EndStation: reader.GetString(5)
            ));
        }

        connection.Close();

        return busLines;
    }

    public List<Bus> GetAllBuses()
    {
        var buses = new List<Bus>();

        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        var selectQuery = "SELECT * FROM Buses";

        using var command = new SQLiteCommand(selectQuery, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            buses.Add(new Bus(
                Id: reader.GetInt32(0),
                Spz: reader.GetString(1),
                BusOperatorId: reader.GetInt32(2),
                Capacity: reader.GetInt32(3)
            ));
        }

        connection.Close();

        return buses;
    }
}