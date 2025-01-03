using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class RoomControl : UserControl
    {
        private string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable roomsTable;

        public RoomControl()
        {
            InitializeComponent();
            LoadRooms();
        }

        // Display rooms in DataGrid
        private void LoadRooms(string query = "SELECT Id, RoomTypeId, RoomNumber, Availability, Description FROM room")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                roomsTable = new DataTable();
                adapter.Fill(roomsTable);
                RoomsDataGrid.ItemsSource = roomsTable.DefaultView;
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

        // Find the current room type based on the ID
        int roomTypeId = Convert.ToInt32(row["RoomType"]);
        RoomType selectedRoomType = roomTypes.FirstOrDefault(rt => rt.Id == roomTypeId);

        // Open the AddUpdateRoomWindow
        AddUpdateRoomWindow popup = new AddUpdateRoomWindow(
            roomTypes,
            selectedRoomType,
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
                    MySqlCommand cmd = new MySqlCommand("UPDATE room SET RoomType = @RoomTypeId, RoomNumber = @RoomNumber, Availability = @Availability, Description = @Description WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@RoomTypeId", updatedRoomType.Id);
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

    }
    
    
}
