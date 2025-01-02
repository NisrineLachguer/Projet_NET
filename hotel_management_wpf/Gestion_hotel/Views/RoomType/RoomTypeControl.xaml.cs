using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class RoomTypeControl : UserControl
    {
        string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable roomTypeTable;

        public RoomTypeControl()
        {
            InitializeComponent();
            LoadRoomTypes();
        }

        // Load room types into DataGrid
        private void LoadRoomTypes(string query = "SELECT Id, Name, Description, Price, Capacity FROM roomtype")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                roomTypeTable = new DataTable();
                adapter.Fill(roomTypeTable);
                RoomTypesDataGrid.ItemsSource = roomTypeTable.DefaultView;
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
    }
}
