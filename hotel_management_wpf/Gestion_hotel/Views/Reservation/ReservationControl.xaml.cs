using System.Data;
using System.IO;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClosedXML.Excel;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class ReservationControl : UserControl
    {
        private readonly string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable reservationTable;
        private int currentPage = 1;
        private const int pageSize = 10;

        public ReservationControl()
        {
            InitializeComponent();
            LoadReservations();
        }

        private void LoadReservations(string query = "SELECT r.Id, c.Name AS Client, ro.RoomNumber AS Room, r.StartDate, r.EndDate, r.TotalCost, rs.Label AS ReservationState FROM reservation r JOIN client c ON r.Client = c.ClientID   JOIN room ro ON r.Room = ro.Id JOIN reservationstate rs ON r.ReservationState = rs.Id")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                reservationTable = new DataTable();
                adapter.Fill(reservationTable);
                ApplyPagination();
            }
        }

        private void ApplyPagination()
        {
            var paginatedData = reservationTable.AsEnumerable()
                                                .Skip((currentPage - 1) * pageSize)
                                                .Take(pageSize)
                                                .CopyToDataTable();
            ReservationsDataGrid.ItemsSource = paginatedData.DefaultView;
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
            if (currentPage * pageSize < reservationTable.Rows.Count)
            {
                currentPage++;
                ApplyPagination();
            }
        }

        private async void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Define email properties
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("aitbelmehdikhaoula@gmail.com"),
                    Subject = "Reservations Report",
                    Body = "Dear Team,\n\nPlease find attached the exported reservations report for your reference.\n\nBest regards,\n[Your Name]",
                    IsBodyHtml = false
                };
                mail.To.Add("k.aitbelmehdi@gmail.com");

                // Create a temporary CSV file for the attachment
                string tempFilePath = Path.Combine(Path.GetTempPath(), "ReservationsExport.csv");
                using (StreamWriter writer = new StreamWriter(tempFilePath))
                {
                    // Write CSV headers
                    var headers = reservationTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                    writer.WriteLine(string.Join(",", headers));

                    // Write rows
                    foreach (DataRow row in reservationTable.Rows)
                    {
                        var fields = row.ItemArray.Select(field => field.ToString().Replace(",", ";")); // Escape commas
                        writer.WriteLine(string.Join(",", fields));
                    }
                }

                // Attach the file
                mail.Attachments.Add(new Attachment(tempFilePath));

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("aitbelmehdikhaoula@gmail.com", "vuec iwcr aymo xaeu"),
                    EnableSsl = true,
                };

                await smtpClient.SendMailAsync(mail);
                MessageBox.Show("Email sent successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}");
            }
        }

        // Filter and Sort Reservations
        private void FilterAndSortReservations()
        {
            /*string query = "SELECT r.Id, c.FirstName AS Client, r.RoomNumber, r.StartDate, r.EndDate, r.TotalCost, rs.Label AS ReservationState FROM reservation r JOIN client c ON r.ClientId = c.Id JOIN reservationstate rs ON r.ReservationStateId = rs.Id";

            if (!string.IsNullOrEmpty(ClientFilterTextBox.Text))
            {
                query += $" WHERE c.FirstName LIKE '%{ClientFilterTextBox.Text}%'";
            }

            if (SortByComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Content.ToString();
                bool ascending = AscendingRadioButton.IsChecked == true;

                query += ascending ? $" ORDER BY {sortBy}" : $" ORDER BY {sortBy} DESC";
            }*/

            //LoadReservations(query);
        }

        // Filter button click
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterAndSortReservations();
        }

        // Reset filters
        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            ClientFilterTextBox.Clear();
            SortByComboBox.SelectedIndex = -1;
            AscendingRadioButton.IsChecked = true;
            LoadReservations();
        }

        // Export to Excel
        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"Reservations_Export_{DateTime.Now:yyyyMMdd}"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    Mouse.OverrideCursor = Cursors.Wait;

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Reservations");

                        // Add headers
                        worksheet.Cell(1, 1).Value = "Client";
                        worksheet.Cell(1, 2).Value = "Room";
                        worksheet.Cell(1, 3).Value = "Start Date";
                        worksheet.Cell(1, 4).Value = "End Date";
                        worksheet.Cell(1, 5).Value = "Total Cost";
                        worksheet.Cell(1, 6).Value = "State";

                        // Style headers
                        var headerRange = worksheet.Range(1, 1, 1, 6);
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                        headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                        // Get data from reservationTable
                        var reservations = reservationTable.AsEnumerable();
                        int row = 2;

                        foreach (var reservation in reservations)
                        {
                            worksheet.Cell(row, 1).Value = reservation["Client"].ToString();
                            worksheet.Cell(row, 2).Value = reservation["Room"].ToString();
                            worksheet.Cell(row, 3).Value = reservation["StartDate"].ToString();
                            worksheet.Cell(row, 4).Value = reservation["EndDate"].ToString();
                            worksheet.Cell(row, 5).Value = reservation["TotalCost"].ToString();
                            worksheet.Cell(row, 6).Value = reservation["ReservationState"].ToString();
                            row++;
                        }

                        // Auto-fit columns
                        worksheet.Columns().AdjustToContents();

                        // Save the file
                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    Mouse.OverrideCursor = Cursors.Arrow;

                    MessageBox.Show("Export completed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
