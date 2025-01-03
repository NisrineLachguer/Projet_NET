using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClosedXML.Excel;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class RoomControl : UserControl
    {
        private string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable roomTable;
        private int currentPage = 1;
        private const int pageSize = 10;
        public RoomControl()
        {
            InitializeComponent();
            LoadRooms();
        }

        // Display rooms in DataGrid
        private void LoadRooms(string query = "SELECT r.Id, r.RoomNumber, r.Availability, r.Description, rt.Id AS RoomTypeId, rt.Name AS RoomTypeName FROM room r JOIN roomtype rt ON r.RoomTypeId = rt.Id")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                roomTable = new DataTable();
                adapter.Fill(roomTable);
                ApplyPagination();
                RoomsDataGrid.ItemsSource = roomTable.DefaultView;
            }
        }


        // Validate input fields
        private bool ValidateInput(string roomType, string roomNumber, bool availability, string description)
        {
            if (string.IsNullOrWhiteSpace(roomType) || string.IsNullOrWhiteSpace(roomNumber) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(roomNumber, out _))
            {
                MessageBox.Show("Room number must be a valid integer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Add a room
     private void AddRoomButton_Click(object sender, RoutedEventArgs e)
{
    // Charger les RoomTypes depuis la base de données
    List<RoomType> roomTypes = new List<RoomType>();
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("SELECT Id, Name FROM RoomType", conn);
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                roomTypes.Add(new RoomType
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name")
                });
            }
        }
    }

    // Afficher le popup pour ajouter une chambre
    AddUpdateRoomWindow popup = new AddUpdateRoomWindow(roomTypes);
    if (popup.ShowDialog() == true)
    {
        // Récupérer le RoomType sélectionné
        RoomType selectedRoomType = popup.RoomTypeComboBox.SelectedItem as RoomType;

        if (selectedRoomType != null && ValidateInput(
                selectedRoomType.Id.ToString(),
                popup.RoomNumberTextBox.Text,
                popup.AvailabilityCheckBox.IsChecked ?? false,
                popup.DescriptionTextBox.Text))
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO room (RoomTypeId, RoomNumber, Availability, Description) VALUES (@RoomTypeId, @RoomNumber, @Availability, @Description)",
                    conn);

                cmd.Parameters.AddWithValue("@RoomTypeId", selectedRoomType.Id);
                cmd.Parameters.AddWithValue("@RoomNumber", popup.RoomNumberTextBox.Text);
                cmd.Parameters.AddWithValue("@Availability", popup.AvailabilityCheckBox.IsChecked ?? false);
                cmd.Parameters.AddWithValue("@Description", popup.DescriptionTextBox.Text);

                cmd.ExecuteNonQuery();
            }

            // Recharger les données après l'ajout
            LoadRooms();
        }
    }
}


        // Update a room
      // Update a room
private void UpdateRoomButton_Click(object sender, RoutedEventArgs e)
{
    if (RoomsDataGrid.SelectedItem != null)
    {
        DataRowView row = (DataRowView)RoomsDataGrid.SelectedItem;

        // Get the list of room types from your data source
        List<RoomType> roomTypes = GetRoomTypes();

        // Find the current room type based on the RoomTypeId (not RoomType)
        int roomTypeId = Convert.ToInt32(row["RoomTypeId"]); // Corrected column name
        RoomType selectedRoomType = roomTypes.FirstOrDefault(rt => rt.Id == roomTypeId);

        // Open the AddUpdateRoomWindow
        AddUpdateRoomWindow popup = new AddUpdateRoomWindow(
            roomTypes,
            selectedRoomType, // Pass the selected room type
            row["RoomNumber"].ToString(),
            (bool)row["Availability"],
            row["Description"].ToString()
        );

        if (popup.ShowDialog() == true)
        {
            if (popup.RoomTypeComboBox.SelectedItem is RoomType updatedRoomType &&
                ValidateInput(updatedRoomType.Id.ToString(), popup.RoomNumberTextBox.Text, popup.AvailabilityCheckBox.IsChecked ?? false, popup.DescriptionTextBox.Text))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE room SET RoomTypeId = @RoomTypeId, RoomNumber = @RoomNumber, Availability = @Availability, Description = @Description WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@RoomTypeId", updatedRoomType.Id); // Corrected parameter name
                    cmd.Parameters.AddWithValue("@RoomNumber", popup.RoomNumberTextBox.Text);
                    cmd.Parameters.AddWithValue("@Availability", popup.AvailabilityCheckBox.IsChecked ?? false);
                    cmd.Parameters.AddWithValue("@Description", popup.DescriptionTextBox.Text);
                    cmd.Parameters.AddWithValue("@Id", row["Id"]);
                    cmd.ExecuteNonQuery();
                }
                LoadRooms();
            }
        }
    }
    else
    {
        MessageBox.Show("Please select a room to update.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}



        // Delete a room
        private void DeleteRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)RoomsDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this room?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM room WHERE Id = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", row["Id"]);
                        cmd.ExecuteNonQuery();
                    }
                    LoadRooms();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Filter and sort rooms
        private void FilterAndSortRooms()
        {
            string query = "SELECT Id, RoomTypeId, RoomNumber, Availability, Description FROM room";

            if (!string.IsNullOrEmpty(RoomTypeFilterTextBox.Text) || !string.IsNullOrEmpty(RoomNumberFilterTextBox.Text))
            {
                query += " WHERE 1=1";
                if (!string.IsNullOrEmpty(RoomTypeFilterTextBox.Text))
                {
                    query += $" AND RoomType LIKE '%{RoomTypeFilterTextBox.Text}%'";
                }
                if (!string.IsNullOrEmpty(RoomNumberFilterTextBox.Text))
                {
                    query += $" AND RoomNumber LIKE '%{RoomNumberFilterTextBox.Text}%'";
                }
            }

            if (SortByComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Content.ToString();
                bool ascending = AscendingRadioButton.IsChecked == true;

                switch (sortBy)
                {
                    case "RoomType":
                        query += ascending ? " ORDER BY RoomType" : " ORDER BY RoomType DESC";
                        break;
                    case "RoomNumber":
                        query += ascending ? " ORDER BY RoomNumber" : " ORDER BY RoomNumber DESC";
                        break;
                }
            }

            LoadRooms(query);
        }

        // Filter button click
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterAndSortRooms();
        }

        // Reset filters
        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            RoomTypeFilterTextBox.Clear();
            RoomNumberFilterTextBox.Clear();
            SortByComboBox.SelectedIndex = -1;
            AscendingRadioButton.IsChecked = true;
            LoadRooms();
        }
        
        private List<RoomType> GetRoomTypes()
        {
            List<RoomType> roomTypes = new List<RoomType>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Id, Name FROM roomtype", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomTypes.Add(new RoomType
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name")
                        });
                    }
                }
            }
            return roomTypes;
        }
        private void ApplyPagination()
        {
            var paginatedData = roomTable.AsEnumerable()
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .CopyToDataTable();
            RoomsDataGrid.ItemsSource = paginatedData.DefaultView;
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
            if (currentPage * pageSize < roomTable.Rows.Count)
            {
                currentPage++;
                ApplyPagination();
            }
        }
        
       private void ExportExcel_Click(object sender, RoutedEventArgs e)
{
    try
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel Files|*.xlsx",
            DefaultExt = "xlsx",
            FileName = $"Rooms_Export_{DateTime.Now:yyyyMMdd}"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            Mouse.OverrideCursor = Cursors.Wait; // Set the cursor to loading

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Rooms");

                // Add headers (adjust these as per the actual column names of your Room data)
                worksheet.Cell(1, 1).Value = "Room Number";
                worksheet.Cell(1, 2).Value = "Room Type";
                worksheet.Cell(1, 3).Value = "Availability";
                worksheet.Cell(1, 4).Value = "Description";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 4);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                // Get data from roomsTable
                var rooms = roomTable.AsEnumerable();
                int row = 2;

                foreach (var room in rooms)
                {
                    worksheet.Cell(row, 1).Value = room["RoomNumber"].ToString();
                    worksheet.Cell(row, 2).Value = room["RoomTypeName"].ToString(); // Assuming RoomTypeName is in the query
                    worksheet.Cell(row, 3).Value = (bool)room["Availability"] ? "Available" : "Not Available";
                    worksheet.Cell(row, 4).Value = room["Description"].ToString();
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
       
private async void SendEmail_Click(object sender, RoutedEventArgs e)
{
    try
    {
        // Define email properties
        MailMessage mail = new MailMessage
        {
            From = new MailAddress("aitbelmehdikhaoula@gmail.com"),
            Subject = "Room Report",
            Body = "Dear Team,\n\nPlease find attached the exported room report for your reference.\n\nBest regards,\n[Your Name]",
            IsBodyHtml = false
        };
        mail.To.Add("k.aitbelmehdi@gmail.com");

        // Create a temporary CSV file for the attachment
        string tempFilePath = Path.Combine(Path.GetTempPath(), "RoomsExport.csv");
        using (StreamWriter writer = new StreamWriter(tempFilePath))
        {
            // Write CSV headers
            var headers = roomTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            writer.WriteLine(string.Join(",", headers));

            // Write rows
            foreach (DataRow row in roomTable.Rows)
            {
                var fields = row.ItemArray.Select(field => field.ToString().Replace(",", ";")); // Escape commas
                writer.WriteLine(string.Join(",", fields));
            }
        }

        // Attach the file
        mail.Attachments.Add(new Attachment(tempFilePath));

        // Configure SMTP client
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new System.Net.NetworkCredential("aitbelmehdikhaoula@gmail.com", "vuec iwcr aymo xaeu"),
            EnableSsl = true,
        };

        // Send the email asynchronously
        await smtpClient.SendMailAsync(mail);

        // Notify the user of success
        MessageBox.Show("Email sent successfully.");
    }
    catch (Exception ex)
    {
        // Notify the user of an error
        MessageBox.Show($"Error sending email: {ex.Message}");
    }
}

       
       
       
    }

}
