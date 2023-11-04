using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Avalonia.Controls.Documents;
using BusLineManager.Core.Data;

namespace BusLineManager.Core.Database;

public static class DbUtils
{
    public static void PrintTablesWithColumns()
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var schemaTables = connection.GetSchema("Tables");

        foreach (DataRow tableRow in schemaTables.Rows)
        {
            var tableName = tableRow["TABLE_NAME"].ToString();
            Console.WriteLine($"Table: {tableName}");

            var schemaColumns = connection.GetSchema("Columns", new[] { null, null, tableName });

            foreach (DataRow columnRow in schemaColumns.Rows)
            {
                var columnName = columnRow["COLUMN_NAME"].ToString();
                Console.WriteLine($"  Column: {columnName}");
            }

            Console.WriteLine();
        }

        connection.Close();
    }
    
    public static void CleanUp()
    {
        using var connection = Database.GetConnection();;
        connection.Open();

        var selectTablesQuery = "SELECT name FROM sqlite_master WHERE type='table'";
        
        using var selectTablesCommand = new SQLiteCommand(selectTablesQuery, connection);
                
        using (var reader = selectTablesCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                var tableName = reader.GetString(0);
                ClearTableData(connection, tableName);
            }
        }

        connection.Close();
    }

    public static void CreateTestData()
    {
        Random random = new Random(69420);
        var conn = new Database();
        var busOperators = new List<BusOperator>
        {
            new(0, "Expres", "101010"),
            new(1, "NeVcas", "202020"),
            new(2, "Erriva", "303030"),
            new(3, "RychloSuperTurboBus", "404040")
        };
        
        foreach (var busOperator in busOperators)
        {
           conn.InsertBusOperator(busOperator);
        }
        
        var id = 0;
        foreach (var busOperator in busOperators)
        {
            var numOfLines = random.Next(3, 9);
            for (int i = 0; i < numOfLines ; i++)
            {
                conn.InsertLine(new BusLine(id,$"Line-{id}",busOperator.Id ?? -1,$"Start{id}",$"End{id}"));
                id++;
            }
        }

        id = 0;
        foreach (var busOperator in busOperators)
        {
            var numOfLines = random.Next(3, 9);
            for (int i = 0; i < numOfLines ; i++)
            {
                conn.InsertBus(new Bus(id,$"SPZ : {id}",busOperator.Id ?? -1,random.Next(50,101)));
                id++;
            }
        }
    }

    public static void ShowDataFromDatabase()
    {
        var conn = new Database();

        Console.WriteLine("BusOperators:");
        /* Zkousel jsem si Span */
        ReadOnlySpan<BusOperator> busOperators = CollectionsMarshal.AsSpan(conn.GetAllBusOperators());
        for (int i = 0; i < busOperators.Length; i++)
        {
            var busOperator = busOperators[i];
            Console.WriteLine($"ID: {busOperator.Id}, Name: {busOperator.Name}, ICO: {busOperator.Ico}");
        }

        Console.WriteLine("\nBusLines:");
        ReadOnlySpan<BusLine> busLines = CollectionsMarshal.AsSpan( conn.GetAllLines());
        for (int i = 0; i < busLines.Length; i++)
        {
            var busLine = busLines[i];
            Console.WriteLine($"ID: {busLine.Id}, Name: {busLine.Name}, BusOperatorID: {busLine.BusOpearatorId}, Start: {busLine.StartStation}, End: {busLine.EndStation}");
        }

        Console.WriteLine("\nBuses:");
        ReadOnlySpan<Bus> buses = CollectionsMarshal.AsSpan(conn.GetAllBuses());
        for (int i = 0; i < buses.Length; i++)
        {
            var bus = buses[i];
            Console.WriteLine($"ID: {bus.Id}, SPZ: {bus.Spz}, BusOperatorID: {bus.BusOperatorId}, Capacity: {bus.Capacity}");
        }
    }
    
    private static void ClearTableData(SQLiteConnection connection, string tableName)
    {
        var deleteQuery = $"DELETE FROM {tableName}";

        using var command = new SQLiteCommand(deleteQuery, connection);
        var rowsAffected = command.ExecuteNonQuery();

        Console.WriteLine(rowsAffected > 0
            ? $"Deletion successful for table '{tableName}'. All records removed."
            : $"No records to delete in table '{tableName}'.");
    }
}