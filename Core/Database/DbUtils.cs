using System;
using System.Data;
using System.Data.SQLite;

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