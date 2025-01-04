using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using WpfApp1.mail_pdf;
using WpfApp1.Views;

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
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Add a client
        private void AjouterBtn_Click(object sender, RoutedEventArgs e)
        {
            addUpdateClientPopWindow popup = new addUpdateClientPopWindow();
            if (popup.ShowDialog() == true)
            {
                if (ValidateInput(popup.NameTextBox.Text, popup.EmailTextBox.Text, popup.PhoneNumberTextBox.Text,
                        popup.AddressTextBox.Text))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd =
                            new MySqlCommand(
                                "INSERT INTO clients (Name, Email, PhoneNumber, Address) VALUES (@Name, @Email, @PhoneNumber, @Address)",
                                conn);
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
                addUpdateClientPopWindow popup = new addUpdateClientPopWindow(
                    row["Name"].ToString(),
                    row["Email"].ToString(),
                    row["PhoneNumber"].ToString(),
                    row["Address"].ToString()
                );

                if (popup.ShowDialog() == true)
                {
                    if (ValidateInput(popup.NameTextBox.Text, popup.EmailTextBox.Text, popup.PhoneNumberTextBox.Text,
                            popup.AddressTextBox.Text))
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand(
                                "UPDATE clients SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address WHERE ClientID = @ClientID",
                                conn);
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
                MessageBox.Show("Please select a client to update.", "Update Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        // Delete a client
        private void SupprimerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ClientsDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this client?",
                    "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

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
                MessageBox.Show("Please select a client to delete.", "Delete Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
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

        private void ExportExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"Clients_Export_{DateTime.Now:yyyyMMdd}"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    Mouse.OverrideCursor = Cursors.Wait;

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Clients");

                        // Add headers
                        worksheet.Cell(1, 1).Value = "Nom";
                        worksheet.Cell(1, 2).Value = "Email";
                        worksheet.Cell(1, 3).Value = "Téléphone";
                        worksheet.Cell(1, 4).Value = "Adresse";

                        // Style headers
                        var headerRange = worksheet.Range(1, 1, 1, 4);
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                        headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                        // Get data from DataGrid
                        var clients = clientsTable.AsEnumerable();
                        int row = 2;

                        foreach (var client in clients)
                        {
                            worksheet.Cell(row, 1).Value = client["Name"].ToString();
                            worksheet.Cell(row, 2).Value = client["Email"].ToString();
                            worksheet.Cell(row, 3).Value = client["PhoneNumber"].ToString();
                            worksheet.Cell(row, 4).Value = client["Address"].ToString();
                            row++;
                        }

                        // Auto-fit columns
                        worksheet.Columns().AdjustToContents();

                        // Save the file
                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Export completed successfully!", "Succès",
                        MessageBoxButton.OK, MessageBoxImage.Information);


                    if (MessageBox.Show("Do you want to open the file?", "Ouvrir",
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
                MessageBox.Show($"Erreur lors de l'export: {ex.Message}", "Erreur",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadClients(string searchTerm = "")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM `clients`";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += " WHERE Name LIKE @SearchTerm";
                }

                query += " ORDER BY Id ASC"; 

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                }

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ClientsDataGrid.ItemsSource = dt.DefaultView;

                //lblClients.Content = $"Total Clients: {dt.Rows.Count}";
            }
        }

        //mail && pdf
        private async void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Show input dialog for recipient email
                var dialog = new InputDialog("Enter recipient email:");
                if (dialog.ShowDialog() == true)
                {
                    string recipientEmail = dialog.ResponseText;
            
                    // Show loading cursor
                    Mouse.OverrideCursor = Cursors.Wait;
            
                    // Generate the PDF content
                    byte[] pdfContent = PdfGenerator.GeneratePdf();

                    // Send email with PDF attachment
                    await EmailSender.SendEmailWithPdfAttachmentAsync(recipientEmail, pdfContent);

                    MessageBox.Show("Email sent successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
