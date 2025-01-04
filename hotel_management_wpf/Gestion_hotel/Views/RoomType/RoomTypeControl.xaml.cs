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
    public partial class RoomTypeControl : UserControl
    {
        private readonly string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable roomTypeTable;
        private int currentPage = 1;
        private const int pageSize = 10;

        public RoomTypeControl()
        {
            InitializeComponent();
            LoadRoomTypes();
        }

        private void LoadRoomTypes(string query = "SELECT Id, Name, Description, Price, Capacity FROM roomtype")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                roomTypeTable = new DataTable();
                adapter.Fill(roomTypeTable);
                ApplyPagination();
            }
        }

        private void ApplyPagination()
        {
            var paginatedData = roomTypeTable.AsEnumerable()
                                             .Skip((currentPage - 1) * pageSize)
                                             .Take(pageSize)
                                             .CopyToDataTable();
            RoomTypesDataGrid.ItemsSource = paginatedData.DefaultView;
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
            if (currentPage * pageSize < roomTypeTable.Rows.Count)
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
                    Subject = "Room Types Report",
                    Body = "Dear Team,\n\nPlease find attached the exported room types report for your reference.\n\nBest regards,\n[Your Name]",
                    IsBodyHtml = false
                };
                mail.To.Add("k.aitbelmehdi@gmail.com");

                // Create a temporary CSV file for the attachment
                string tempFilePath = Path.Combine(Path.GetTempPath(), "RoomTypesExport.csv");
                using (StreamWriter writer = new StreamWriter(tempFilePath))
                {
                    // Write CSV headers
                    var headers = roomTypeTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                    writer.WriteLine(string.Join(",", headers));

                    // Write rows
                    foreach (DataRow row in roomTypeTable.Rows)
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
        
        
        
        
        
        
        
        // Validate input fields
        private bool ValidateInput(string name, string description, string price, string capacity)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(capacity))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(price, out _) || !int.TryParse(capacity, out _))
            {
                MessageBox.Show("Price must be a valid decimal number and Capacity must be a valid integer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Add a new room type
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateRoomTypePopup popup = new AddUpdateRoomTypePopup();
            if (popup.ShowDialog() == true)
            {
                if (ValidateInput(popup.NameTextBox.Text, popup.DescriptionTextBox.Text, popup.PriceTextBox.Text, popup.CapacityTextBox.Text))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO roomtype (Name, Description, Price, Capacity) VALUES (@Name, @Description, @Price, @Capacity)", conn);
                        cmd.Parameters.AddWithValue("@Name", popup.NameTextBox.Text);
                        cmd.Parameters.AddWithValue("@Description", popup.DescriptionTextBox.Text);
                        cmd.Parameters.AddWithValue("@Price", decimal.Parse(popup.PriceTextBox.Text));
                        cmd.Parameters.AddWithValue("@Capacity", int.Parse(popup.CapacityTextBox.Text));
                        cmd.ExecuteNonQuery();
                    }
                    LoadRoomTypes();
                }
            }
        }

        // Update an existing room type
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomTypesDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)RoomTypesDataGrid.SelectedItem;
                AddUpdateRoomTypePopup popup = new AddUpdateRoomTypePopup(
                    row["Name"].ToString(),
                    row["Description"].ToString(),
                    row["Price"].ToString(),
                    row["Capacity"].ToString()
                );

                if (popup.ShowDialog() == true)
                {
                    if (ValidateInput(popup.NameTextBox.Text, popup.DescriptionTextBox.Text, popup.PriceTextBox.Text, popup.CapacityTextBox.Text))
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("UPDATE roomtype SET Name = @Name, Description = @Description, Price = @Price, Capacity = @Capacity WHERE Id = @Id", conn);
                            cmd.Parameters.AddWithValue("@Name", popup.NameTextBox.Text);
                            cmd.Parameters.AddWithValue("@Description", popup.DescriptionTextBox.Text);
                            cmd.Parameters.AddWithValue("@Price", decimal.Parse(popup.PriceTextBox.Text));
                            cmd.Parameters.AddWithValue("@Capacity", int.Parse(popup.CapacityTextBox.Text));
                            cmd.Parameters.AddWithValue("@Id", row["Id"]);
                            cmd.ExecuteNonQuery();
                        }
                        LoadRoomTypes();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room type to update.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete a room type
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomTypesDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)RoomTypesDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this room type?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM roomtype WHERE Id = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", row["Id"]);
                        cmd.ExecuteNonQuery();
                    }
                    LoadRoomTypes();
                }
            }
            else
            {
                MessageBox.Show("Please select a room type to delete.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Filter and Sort Room Types
        private void FilterAndSortRoomTypes()
        {
            string query = "SELECT Id, Name, Description, Price, Capacity FROM roomtype";

            if (!string.IsNullOrEmpty(NameFilterTextBox.Text))
            {
                query += $" WHERE Name LIKE '%{NameFilterTextBox.Text}%'";
            }

            if (SortByComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Content.ToString();
                bool ascending = AscendingRadioButton.IsChecked == true;

                query += ascending ? $" ORDER BY {sortBy}" : $" ORDER BY {sortBy} DESC";
            }

            LoadRoomTypes(query);
        }

        // Filter button click
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterAndSortRoomTypes();
        }

        // Reset filters
        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            NameFilterTextBox.Clear();
            SortByComboBox.SelectedIndex = -1;
            AscendingRadioButton.IsChecked = true;
            LoadRoomTypes();
        }
       private void ExportExcel_Click(object sender, RoutedEventArgs e)
{
    try
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel Files|*.xlsx",
            DefaultExt = "xlsx",
            FileName = $"RoomTypes_Export_{DateTime.Now:yyyyMMdd}"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            Mouse.OverrideCursor = Cursors.Wait;  // Set the cursor to loading

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("RoomTypes");

                // Add headers (adjust these as per the actual column names of your RoomType data)
                worksheet.Cell(1, 1).Value = "Room Type Name";
                worksheet.Cell(1, 2).Value = "Description";
                worksheet.Cell(1, 4).Value = "Price per Night";
                worksheet.Cell(1, 3).Value = "Capacity";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 4);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                // Get data from roomTypeTable
                var roomTypes = roomTypeTable.AsEnumerable();
                int row = 2;

                foreach (var roomType in roomTypes)
                {
                    worksheet.Cell(row, 1).Value = roomType["Name"].ToString();
                    worksheet.Cell(row, 2).Value = roomType["Description"].ToString();
                    worksheet.Cell(row, 4).Value = roomType["Price"].ToString();
                    worksheet.Cell(row, 3).Value = roomType["Capacity"].ToString();
                    row++;
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                // Save the file
                workbook.SaveAs(saveFileDialog.FileName);
            }

            // Reset cursor to default after completion
            Mouse.OverrideCursor = Cursors.Arrow;

            MessageBox.Show("Export completed successfully!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // Optional: Open the file
            if (MessageBox.Show("Do you want to open the file?", "Open File",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = saveFileDialog.FileName,
                    UseShellExecute = true
                });
            }
        }
    }
    catch (Exception ex)
    {
        // Reset cursor to default in case of error
        Mouse.OverrideCursor = Cursors.Arrow;

        MessageBox.Show($"An error occurred during the export: {ex.Message}", "Error",
            MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
        
    }
}