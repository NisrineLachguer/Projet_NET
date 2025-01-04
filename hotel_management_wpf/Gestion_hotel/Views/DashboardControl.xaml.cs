using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace WpfApp1.Views
{
    public partial class DashboardControl : UserControl
    {
        private readonly string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";

        public DashboardControl()
        {
            InitializeComponent();
            LoadEmployeesPieChart();
            LoadClientsPieChart();
        }

        private void LoadEmployeesPieChart()
        {
            var roleCounts = new Dictionary<string, int>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT role, COUNT(*) AS count FROM employees GROUP BY role;";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string role = reader["role"].ToString();
                        int count = int.Parse(reader["count"].ToString());
                        roleCounts[role] = count;
                    }
                }
            }

            EmployeesPieChart.Series = new SeriesCollection();
            foreach (var role in roleCounts)
            {
                EmployeesPieChart.Series.Add(new PieSeries
                {
                    Title = role.Key,
                    Values = new ChartValues<int> { role.Value },
                    DataLabels = true
                });
            }
        }

        private void LoadClientsPieChart()
        {
            var dateCounts = new Dictionary<string, int>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DATE(created_at) AS date, COUNT(*) AS count FROM clients GROUP BY DATE(created_at);";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string date = reader["date"].ToString();
                        int count = int.Parse(reader["count"].ToString());
                        dateCounts[date] = count;
                    }
                }
            }

            ClientsPieChart.Series = new SeriesCollection();
            foreach (var date in dateCounts)
            {
                ClientsPieChart.Series.Add(new PieSeries
                {
                    Title = date.Key,
                    Values = new ChartValues<int> { date.Value },
                    DataLabels = true
                });
            }
        }
    }
}
