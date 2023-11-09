using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
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
        command.Parameters.AddWithValue("@ICO", busOperator.Ico);
        command.ExecuteNonQuery();

        connection.Close();
    }

    public void InsertLine(BusLine busLine)
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        var insertQuery = @"INSERT INTO Lines (Name, BusOperatorID, StartStation, EndStation) 
                            VALUES (@Name, @BusOperatorID, @StartStation, @EndStation)";
    
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

        string insertQuery = "INSERT INTO BusesNew (SPZ, Capacity, BusOperatorId, LineId) VALUES (@SPZ, @Capacity,@BusOperatorID,@LineId)";
    
        using var command = new SQLiteCommand(insertQuery, connection);

        command.Parameters.AddWithValue("@SPZ", bus.Spz);
        command.Parameters.AddWithValue("@Capacity", bus.Capacity);
        command.Parameters.AddWithValue("@BusOperatorID", bus.BusOperatorId);
        command.Parameters.AddWithValue("@LineId", bus.LineId);
    
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

        var selectQuery = "SELECT * FROM BusesNew";

        using var command = new SQLiteCommand(selectQuery, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            buses.Add(new Bus(
                Id: reader.GetInt64(0),
                Spz: reader.GetString(1),
                Capacity: reader.GetInt32(2),
                BusOperatorId: reader.GetInt64(3),
                LineId: reader.GetInt64(4)
            ));
        }

        connection.Close();

        return buses;
    }

    public async Task<List<BusLine>> GetLinesForOperatorAsync(BusOperator busOperator)
    {
        var lines = new List<BusLine>();
        await using var connection = new SQLiteConnection(ConnectionString);
        
        await connection.OpenAsync();

        const string selectQuery = "SELECT * FROM Lines WHERE BusOperatorID = @BusOperatorID";

        await using var command = new SQLiteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@BusOperatorID", busOperator.Id);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var line = new BusLine(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                BusOpearatorId: reader.GetInt64(2),
                StartStation: reader.GetString(4),
                EndStation: reader.GetString(5)
                );

            lines.Add(line);
        }

        return lines;
    }
    
    public async Task<BusLine?> GetLineByNameAsync(string name)
    {
        try
        {
            await using var connection = new SQLiteConnection(ConnectionString);
        
            await connection.OpenAsync();

            const string selectQuery = "SELECT * FROM Lines WHERE Name = @Name";

            await using var command = new SQLiteCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@Name", name);

            await using var reader = await command.ExecuteReaderAsync();
        
            await reader.ReadAsync();

            return new BusLine(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                BusOpearatorId: reader.GetInt64(2),
                StartStation: reader.GetString(4),
                EndStation: reader.GetString(5)
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<List<Bus>> GetBusesForLineAsync(BusLine line)
    {
        var buses = new List<Bus>();
        await using var connection = new SQLiteConnection(ConnectionString);
        
        await connection.OpenAsync();

        const string selectQuery = "SELECT * FROM BusesNew WHERE LineId = @LineId";

        await using var command = new SQLiteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@LineId", line.Id);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {

            buses.Add(new Bus(
                Id: reader.GetInt64(0),
                Spz: reader.GetString(1),
                Capacity: reader.GetInt32(2),
                BusOperatorId: reader.GetInt64(3),
                LineId: reader.GetInt64(4)
            ));
        }
        
        return buses;
    }

    public string GetBusOperatorNameById(long id)
    {
         using var connection = new SQLiteConnection(ConnectionString);
        
        connection.Open();

        const string selectQuery = "SELECT * FROM BusOperators WHERE ID = @ID";

        using var command = new SQLiteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@ID", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var name = reader["Name"].ToString() ?? string.Empty;
        
        connection.Close();
        return name;
    }
    
}