using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace WpfApp1.Views;

public partial class DashboardControl : UserControl
{
     private readonly string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";

        public DashboardControl()
        {
            InitializeComponent();
        }

        private readonly Dictionary<string, (string Color, string Meaning)> roleColors =
            new Dictionary<string, (string, string)>
            {
                { "Hotel Manager", ("#FF4081", "Senior management responsible for overall operations") },
                { "Front Desk Manager", ("#3F51B5", "Supervises reception and guest services") },
                { "Receptionist", ("#009688", "Handles check-ins, check-outs, and guest inquiries") },
                { "Housekeeping Manager", ("#FFC107", "Oversees cleaning and maintenance staff") },
                { "Housekeeper", ("#9C27B0", "Maintains cleanliness of rooms and facilities") }
            };

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadStatistics();
            //LoadEmployeeRolesChart();
            //LoadRecentClients();
            LoadEmployeeStats();
            
        }

        private void LoadStatistics()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Get total employees
                using (var command = new MySqlCommand("SELECT COUNT(*) FROM employees", connection))
                {
                    int totalEmployees = Convert.ToInt32(command.ExecuteScalar());
                    TotalEmployeesText.Text = totalEmployees.ToString();
                }

                // Get total clients
                using (var command = new MySqlCommand("SELECT COUNT(*) FROM clients", connection))
                {
                    int totalClients = Convert.ToInt32(command.ExecuteScalar());
                    TotalClientsText.Text = totalClients.ToString();
                }
            }
        }
    
        private void LoadEmployeeStats()
        {
            var roleDistribution = new Dictionary<string, int>();
            int totalEmployees = 0;

            // Get data from database
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT role, COUNT(*) as count FROM employees GROUP BY role", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string role = reader["role"].ToString();
                            int count = Convert.ToInt32(reader["count"]);
                            roleDistribution[role] = count;
                            totalEmployees += count;
                        }
                    }
                }
            }

            // Update total employees text
            TotalEmployeesText.Text = $"Total Employees: {totalEmployees}";

            // Clear previous chart
            ChartCanvas.Children.Clear();
            LegendPanel.Children.Clear();
            LegendPanel.Children.Add(new TextBlock 
            { 
                Text = "Legend",
                Style = (Style)FindResource("MaterialDesignSubtitle1TextBlock"),
                Margin = new Thickness(0, 0, 0, 16)
            });

            // Calculate total for percentages
            double total = 0;
            foreach (var count in roleDistribution.Values)
            {
                total += count;
            }

            // Draw pie chart
            double startAngle = 0;
            double centerX = ChartCanvas.ActualWidth / 2;
            double centerY = ChartCanvas.ActualHeight / 2;
            double radius = Math.Min(centerX, centerY) - 20;

            foreach (var role in roleDistribution)
            {
                if (!roleColors.ContainsKey(role.Key)) continue;

                double percentage = role.Value / total;
                double sweepAngle = percentage * 360;

                // Create pie slice
                var slice = CreatePieSlice(
                    centerX, centerY,
                    radius,
                    startAngle,
                    sweepAngle,
                    roleColors[role.Key].Color);

                ChartCanvas.Children.Add(slice);

                // Add to legend
                AddLegendItem(
                    role.Key,
                    roleColors[role.Key].Color,
                    roleColors[role.Key].Meaning,
                    $"{role.Value} ({percentage:P1})");

                startAngle += sweepAngle;
            }
        }

        private Path CreatePieSlice(double centerX, double centerY, double radius, double startAngle, double sweepAngle, string colorHex)
        {
            var path = new Path
            {
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex)),
                Stroke = Brushes.White,
                StrokeThickness = 2
            };

            var figure = new PathFigure
            {
                StartPoint = new Point(centerX, centerY)
            };

            startAngle = startAngle * Math.PI / 180;
            sweepAngle = sweepAngle * Math.PI / 180;

            figure.Segments.Add(new LineSegment(
                new Point(
                    centerX + radius * Math.Cos(startAngle),
                    centerY + radius * Math.Sin(startAngle)
                ), true));

            figure.Segments.Add(new ArcSegment(
                new Point(
                    centerX + radius * Math.Cos(startAngle + sweepAngle),
                    centerY + radius * Math.Sin(startAngle + sweepAngle)
                ),
                new Size(radius, radius),
                0,
                sweepAngle > Math.PI,
                SweepDirection.Clockwise,
                true));

            figure.Segments.Add(new LineSegment(new Point(centerX, centerY), true));

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            path.Data = geometry;

            return path;
        }

        private void AddLegendItem(string role, string colorHex, string meaning, string stats)
        {
            var legendItem = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 8)
            };

            // Color box
            var colorBox = new Rectangle
            {
                Width = 16,
                Height = 16,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex)),
                Margin = new Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            // Text container
            var textContainer = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            // Role and stats
            var roleText = new TextBlock
            {
                Text = $"{role} - {stats}",
                Style = (Style)FindResource("MaterialDesignBody1TextBlock")
            };

            // Meaning
            var meaningText = new TextBlock
            {
                Text = meaning,
                Style = (Style)FindResource("MaterialDesignCaptionTextBlock"),
                Opacity = 0.6,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 200
            };

            textContainer.Children.Add(roleText);
            textContainer.Children.Add(meaningText);

            legendItem.Children.Add(colorBox);
            legendItem.Children.Add(textContainer);

            LegendPanel.Children.Add(legendItem);
        }
    
        
}