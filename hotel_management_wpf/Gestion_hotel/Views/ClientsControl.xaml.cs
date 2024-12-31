using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class ClientsControl : UserControl
    {
        string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable clientsTable;

        public ClientsControl()
        {
            InitializeComponent();
            AfficherClients();
        }

        // Display clients in DataGrid
        private void AfficherClients(string query = "SELECT ClientID, Name, Email, PhoneNumber, Address FROM clients")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                clientsTable = new DataTable();
                adapter.Fill(clientsTable);
                ClientsDataGrid.ItemsSource = clientsTable.DefaultView;
            }
        }

        // Validate input fields
        private bool ValidateInput(string name, string email, string phoneNumber, string address)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Add a client
        private void AjouterBtn_Click(object sender, RoutedEventArgs e)
        {
            addUpdatePopWindow popup = new addUpdatePopWindow();
            if (popup.ShowDialog() == true)
            {
                if (ValidateInput(popup.NameTextBox.Text, popup.EmailTextBox.Text, popup.PhoneNumberTextBox.Text, popup.AddressTextBox.Text))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO clients (Name, Email, PhoneNumber, Address) VALUES (@Name, @Email, @PhoneNumber, @Address)", conn);
                        cmd.Parameters.AddWithValue("@Name", popup.NameTextBox.Text);
                        cmd.Parameters.AddWithValue("@Email", popup.EmailTextBox.Text);
                        cmd.Parameters.AddWithValue("@PhoneNumber", popup.PhoneNumberTextBox.Text);
                        cmd.Parameters.AddWithValue("@Address", popup.AddressTextBox.Text);
                        cmd.ExecuteNonQuery();
                    }
                    AfficherClients();
                }
            }
        }

        // Update a client
        private void ModifierBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ClientsDataGrid.SelectedItem;
                addUpdatePopWindow popup = new addUpdatePopWindow(
                    row["Name"].ToString(),
                    row["Email"].ToString(),
                    row["PhoneNumber"].ToString(),
                    row["Address"].ToString()
                );

                if (popup.ShowDialog() == true)
                {
                    if (ValidateInput(popup.NameTextBox.Text, popup.EmailTextBox.Text, popup.PhoneNumberTextBox.Text, popup.AddressTextBox.Text))
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("UPDATE clients SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address WHERE ClientID = @ClientID", conn);
                            cmd.Parameters.AddWithValue("@Name", popup.NameTextBox.Text);
                            cmd.Parameters.AddWithValue("@Email", popup.EmailTextBox.Text);
                            cmd.Parameters.AddWithValue("@PhoneNumber", popup.PhoneNumberTextBox.Text);
                            cmd.Parameters.AddWithValue("@Address", popup.AddressTextBox.Text);
                            cmd.Parameters.AddWithValue("@ClientID", row["ClientID"]);
                            cmd.ExecuteNonQuery();
                        }
                        AfficherClients();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a client to update.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete a client
        private void SupprimerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ClientsDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this client?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM clients WHERE ClientID = @ClientID", conn);
                        cmd.Parameters.AddWithValue("@ClientID", row["ClientID"]);
                        cmd.ExecuteNonQuery();
                    }
                    AfficherClients();
                }
            }
            else
            {
                MessageBox.Show("Please select a client to delete.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Filter and Sort Clients
        private void FilterAndSortClients()
        {
            string query = "SELECT ClientID, Name, Email, PhoneNumber, Address FROM clients";

            if (!string.IsNullOrEmpty(NameFilterTextBox.Text) || !string.IsNullOrEmpty(EmailFilterTextBox.Text))
            {
                query += " WHERE 1=1";
                if (!string.IsNullOrEmpty(NameFilterTextBox.Text))
                {
                    query += $" AND Name LIKE '%{NameFilterTextBox.Text}%'";
                }
                if (!string.IsNullOrEmpty(EmailFilterTextBox.Text))
                {
                    query += $" AND Email LIKE '%{EmailFilterTextBox.Text}%'";
                }
            }

            if (SortByComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Content.ToString();
                bool ascending = AscendingRadioButton.IsChecked == true;

                switch (sortBy)
                {
                    case "Name":
                        query += ascending ? " ORDER BY Name" : " ORDER BY Name DESC";
                        break;
                    case "Email":
                        query += ascending ? " ORDER BY Email" : " ORDER BY Email DESC";
                        break;
                }
            }

            AfficherClients(query);
        }

        // Filter button click
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterAndSortClients();
        }

        // Reset filters
        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            NameFilterTextBox.Clear();
            EmailFilterTextBox.Clear();
            SortByComboBox.SelectedIndex = -1;
            AscendingRadioButton.IsChecked = true;
            AfficherClients();
        }
    }
}
