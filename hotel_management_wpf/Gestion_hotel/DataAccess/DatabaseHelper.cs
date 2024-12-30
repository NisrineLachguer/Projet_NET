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
}
