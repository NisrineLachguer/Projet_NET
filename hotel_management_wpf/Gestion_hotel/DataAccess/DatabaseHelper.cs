namespace WpfApp1.DataAccess;

using System.Data;
using MySql.Data.MySqlClient;

public static class DatabaseHelper
{
    private static readonly string ConnectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
    
    public static DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                using (var adapter = new MySqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }

    public static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
        }
    }
}
