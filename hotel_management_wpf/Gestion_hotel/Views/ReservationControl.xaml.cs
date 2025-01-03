using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class ReservationsControl : UserControl
    {
        string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private DataTable reservationsTable;

        public ReservationsControl()
        {
            InitializeComponent();
            AfficherReservations();
        }

      

        // Display reservations in DataGrid
        private void AfficherReservations(string query = "SELECT ReservationID, ClientName, ReservationDate, Status FROM reservations")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                reservationsTable = new DataTable();
                adapter.Fill(reservationsTable);
                ReservationsDataGrid.ItemsSource = reservationsTable.DefaultView;
            }
        }

        // Validate input fields
        private bool ValidateInput(string clientName, string reservationDate, string status)
        {
            if (string.IsNullOrWhiteSpace(clientName) || string.IsNullOrWhiteSpace(reservationDate) || string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!DateTime.TryParse(reservationDate, out _))
            {
                MessageBox.Show("Invalid date format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Add a reservation
        private void AjouterBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateReservationPopWindow popup = new AddUpdateReservationPopWindow();
            if (popup.ShowDialog() == true)
            {
                if (popup.ClientNameTextBox?.Text == null || popup.StatusComboBox?.SelectedItem == null)
                {
                    MessageBox.Show("Remplis les champs koulhom.");
                    return;
                }

                if (ValidateInput(popup.ClientNameTextBox.Text, popup.ReservationDateTextBox.Text, popup.StatusComboBox.SelectedItem.ToString()))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO reservations (ClientName, ReservationDate, Status) VALUES (@ClientName, @ReservationDate, @Status)", conn);
                        cmd.Parameters.AddWithValue("@ClientName", popup.ClientNameTextBox.Text);
                        cmd.Parameters.AddWithValue("@ReservationDate", popup.ReservationDateTextBox.Text);
                        cmd.Parameters.AddWithValue("@Status", popup.StatusComboBox.SelectedItem?.ToString());
                        cmd.ExecuteNonQuery();
                    }
                    AfficherReservations();
                }
            }
        }


        // Update a reservation
        private void ModifierBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ReservationsDataGrid.SelectedItem;
                AddUpdateReservationPopWindow popup = new AddUpdateReservationPopWindow(
                    row["ClientName"].ToString(),
                    row["ReservationDate"].ToString(),
                    row["Status"].ToString()
                );

                if (popup.ShowDialog() == true)
                {
                    if (ValidateInput(popup.ClientNameTextBox.Text, popup.ReservationDateTextBox.Text, popup.StatusComboBox.SelectedItem.ToString()))
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("UPDATE reservations SET ClientName = @ClientName, ReservationDate = @ReservationDate, Status = @Status WHERE ReservationID = @ReservationID", conn);
                            cmd.Parameters.AddWithValue("@ClientName", popup.ClientNameTextBox.Text);
                            cmd.Parameters.AddWithValue("@ReservationDate", popup.ReservationDateTextBox.Text);
                            cmd.Parameters.AddWithValue("@Status", popup.StatusComboBox.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@ReservationID", row["ReservationID"]);
                            cmd.ExecuteNonQuery();
                        }
                        AfficherReservations();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to update.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete a reservation
        private void SupprimerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ReservationsDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this reservation?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM reservations WHERE ReservationID = @ReservationID", conn);
                        cmd.Parameters.AddWithValue("@ReservationID", row["ReservationID"]);
                        cmd.ExecuteNonQuery();
                    }
                    AfficherReservations();
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to delete.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Filter and Sort Reservations
        private void FilterAndSortReservations()
        {
            string query = "SELECT ReservationID, ClientName, ReservationDate, Status FROM reservations";

            if (!string.IsNullOrEmpty(ClientNameFilterTextBox.Text))
            {
                query += " WHERE ClientName LIKE '%" + ClientNameFilterTextBox.Text + "%'";
            }

            if (SortByComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Content.ToString();
                bool ascending = AscendingRadioButton.IsChecked == true;

                switch (sortBy)
                {
                    case "ClientName":
                        query += ascending ? " ORDER BY ClientName" : " ORDER BY ClientName DESC";
                        break;
                    case "ReservationDate":
                        query += ascending ? " ORDER BY ReservationDate" : " ORDER BY ReservationDate DESC";
                        break;
                }
            }

            AfficherReservations(query);
        }

        // Filter button click
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterAndSortReservations();
        }

        // Reset filters
        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            ClientNameFilterTextBox.Clear();
            SortByComboBox.SelectedIndex = -1;
            AscendingRadioButton.IsChecked = true;
            AfficherReservations();
        }
    }

    internal class AscendingRadioButton
    {
        public static bool IsChecked { get; set; }
    }

    internal class SortByComboBox
    {
        public static object SelectedItem { get; set; }
        public static int SelectedIndex { get; set; }
    }

    internal class ClientNameFilterTextBox
    {
        public static string? Text { get; set; }

        public static void Clear()
        {
            throw new NotImplementedException();
        }
    }


    internal class ReservationsDataGrid
    {
        public static DataView ItemsSource { get; set; }
        public static DataRowView SelectedItem { get; set; }
    }
}
