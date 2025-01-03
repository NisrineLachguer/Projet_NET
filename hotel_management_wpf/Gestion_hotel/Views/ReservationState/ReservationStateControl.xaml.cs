using System.Data;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class ReservationStateControl : UserControl
    {
        private readonly string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable reservationStateTable;
        private int currentPage = 1;
        private const int pageSize = 10;

        public ReservationStateControl()
        {
            InitializeComponent();
            LoadReservationStates();
        }

        private void LoadReservationStates(string query = "SELECT Id, Code, Label FROM reservationstate")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                reservationStateTable = new DataTable();
                adapter.Fill(reservationStateTable);
                ApplyPagination();
            }
        }

        private void ApplyPagination()
        {
            var paginatedData = reservationStateTable.AsEnumerable()
                                                     .Skip((currentPage - 1) * pageSize)
                                                     .Take(pageSize)
                                                     .CopyToDataTable();
            ReservationStatesDataGrid.ItemsSource = paginatedData.DefaultView;
            PageLabel.Content = $"Page {currentPage}";
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                ApplyPagination();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage * pageSize < reservationStateTable.Rows.Count)
            {
                currentPage++;
                ApplyPagination();
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT Id, Code, Label FROM reservationstate WHERE 1=1";

            if (!string.IsNullOrEmpty(CodeFilterTextBox.Text))
            {
                query += $" AND Code LIKE '%{CodeFilterTextBox.Text}%'";
            }

            if (!string.IsNullOrEmpty(LabelFilterTextBox.Text))
            {
                query += $" AND Label LIKE '%{LabelFilterTextBox.Text}%'";
            }

            LoadReservationStates(query);
        }

        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            CodeFilterTextBox.Clear();
            LabelFilterTextBox.Clear();
            LoadReservationStates();
        }

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"ReservationStates_Export_{DateTime.Now:yyyyMMdd}"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("ReservationStates");
                        worksheet.Cell(1, 1).Value = "Code";
                        worksheet.Cell(1, 2).Value = "Label";

                        for (int i = 0; i < reservationStateTable.Rows.Count; i++)
                        {
                            worksheet.Cell(i + 2, 1).Value = reservationStateTable.Rows[i]["Code"];
                            worksheet.Cell(i + 2, 2).Value = reservationStateTable.Rows[i]["Label"];
                        }

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Export completed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during export: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }
    }
}
