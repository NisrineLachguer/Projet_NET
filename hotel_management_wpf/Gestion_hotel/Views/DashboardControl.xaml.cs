using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1.Views
{
    public partial class DashboardControl : UserControl, INotifyPropertyChanged
    {
        string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private SeriesCollection _employeesPieSeries;
        private SeriesCollection _clientsColumnSeries;
        private readonly DispatcherTimer timer;
        private List<string> _labels;
        private Func<double, string> _yFormatter;

        // Properties for data binding
        public SeriesCollection EmployeesPieSeries
        {
            get => _employeesPieSeries;
            set
            {
                _employeesPieSeries = value;
                OnPropertyChanged(nameof(EmployeesPieSeries));
            }
        }

        public SeriesCollection ClientsColumnSeries
        {
            get => _clientsColumnSeries;
            set
            {
                _clientsColumnSeries = value;
                OnPropertyChanged(nameof(ClientsColumnSeries));
            }
        }

        public List<string> Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        public Func<double, string> YFormatter
        {
            get => _yFormatter;
            set
            {
                _yFormatter = value;
                OnPropertyChanged(nameof(YFormatter));
            }
        }

        private readonly Dictionary<string, (Color color, string meaning)> roleColors = new Dictionary<string, (Color, string)>
        {
            { "Housekeeper", (Color.FromRgb(41, 98, 255), "Cleaning and Room Maintenance") },         // Royal Blue
            { "Housekeeping Manager", (Color.FromRgb(0, 200, 83), "Department Supervision") },        // Green
            { "Front Desk Manager", (Color.FromRgb(255, 82, 82), "Guest Services Management") },      // Red
            { "Hotel Manager", (Color.FromRgb(156, 39, 176), "Strategic Operations") }                // Purple
        };

        public DashboardControl()
        {
            InitializeComponent();
            DataContext = this;
            
            // Initialize charts
            InitializeCharts();

            // Set up timer for live updates
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1) // Update every minute
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeCharts()
        {
            LoadEmployeeData();
            LoadClientData();
            YFormatter = value => value.ToString("N0");
        }

        private void LoadEmployeeData()
        {
            var series = new SeriesCollection();

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            role,
                            COUNT(*) as employee_count
                        FROM employees
                        GROUP BY role
                        ORDER BY role";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string role = reader["role"].ToString();
                            int count = Convert.ToInt32(reader["employee_count"]);

                            var roleInfo = roleColors[role];

                            series.Add(new PieSeries
                            {
                                Title = $"{role} ({count})",
                                Values = new ChartValues<double> { count },
                                DataLabels = true,
                                Fill = new SolidColorBrush(roleInfo.color),
                                LabelPoint = point => count.ToString(),
                                FontSize = 12,
                                LabelPosition = PieLabelPosition.InsideSlice
                            });
                        }
                    }

                    EmployeesPieSeries = series;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error loading employee data: {ex.Message}");
                }
            }
        }

        private void LoadClientData()
        {
            var values = new ChartValues<double>();
            var dates = new List<string>();

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            DATE_FORMAT(created_at, '%m/%d') as date,
                            COUNT(*) as client_count
                        FROM clients
                        WHERE created_at >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
                        GROUP BY DATE(created_at)
                        ORDER BY created_at";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = reader["date"].ToString();
                            var count = Convert.ToDouble(reader["client_count"]);

                            dates.Add(date);
                            values.Add(count);
                        }
                    }

                    Labels = dates;
                    // Update the client chart color in LoadClientData():
                    ClientsColumnSeries = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Clients",
                            Values = values,
                            Fill = new SolidColorBrush(Color.FromRgb(33, 150, 243)), // Bright Blue
                            MaxColumnWidth = 50,
                            ColumnPadding = 5
                        }
                    };
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error loading client data: {ex.Message}");
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LoadClientData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            timer.Stop();
        }
    }
}