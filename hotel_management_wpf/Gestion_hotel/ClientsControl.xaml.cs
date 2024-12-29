using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class ClientsControl : UserControl
    {
        // Connection string to the MySQL database
        private string _connectionString = "Server=localhost;Database=hotel_management_wpf;Uid=root;Pwd=;";

        public ClientsControl()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            // Fetch clients from the database and bind to the DataGrid
            var clients = GetClientsFromDatabase();
            ClientsDataGrid.ItemsSource = clients;
        }

        private List<Client> GetClientsFromDatabase()
        {
            var clients = new List<Client>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT ClientID, Name, Email, PhoneNumber, Address FROM clients";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            ClientID = reader.GetInt32("ClientID"),
                            Name = reader.GetString("Name"),
                            Email = reader.GetString("Email"),
                            PhoneNumber = reader.GetString("PhoneNumber"),
                            Address = reader.GetString("Address")
                        });
                    }
                }
            }

            return clients;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchBox.Text;

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT ClientID, Name, Email, PhoneNumber, Address " +
                            "FROM clients WHERE Name LIKE @Search OR Email LIKE @Search";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Search", $"%{searchText}%");

                    var clients = new List<Client>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                ClientID = reader.GetInt32("ClientID"),
                                Name = reader.GetString("Name"),
                                Email = reader.GetString("Email"),
                                PhoneNumber = reader.GetString("PhoneNumber"),
                                Address = reader.GetString("Address")
                            });
                        }
                    }

                    ClientsDataGrid.ItemsSource = clients;
                }
            }
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (SortByComboBox.SelectedItem is ComboBoxItem selected)
            {
                string sortBy = selected.Content.ToString();
                string columnName = sortBy == "Name" ? "Name" : "Email";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = $"SELECT ClientID, Name, Email, PhoneNumber, Address FROM clients ORDER BY {columnName}";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        var clients = new List<Client>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clients.Add(new Client
                                {
                                    ClientID = reader.GetInt32("ClientID"),
                                    Name = reader.GetString("Name"),
                                    Email = reader.GetString("Email"),
                                    PhoneNumber = reader.GetString("PhoneNumber"),
                                    Address = reader.GetString("Address")
                                });
                            }
                        }
                        ClientsDataGrid.ItemsSource = clients;
                    }
                }
            }*/
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "INSERT INTO clients (Name, Email, PhoneNumber, Address) VALUES (@Name, @Email, @PhoneNumber, @Address)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", "New Client");
                    command.Parameters.AddWithValue("@Email", "new@example.com");
                    command.Parameters.AddWithValue("@PhoneNumber", "000000000");
                    command.Parameters.AddWithValue("@Address", "New Address");

                    command.ExecuteNonQuery();
                }
            }

            LoadClients(); // Refresh the DataGrid
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Client selectedClient)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "UPDATE clients SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address WHERE ClientID = @ClientID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", "Edited Name");
                        command.Parameters.AddWithValue("@Email", "edited@example.com");
                        command.Parameters.AddWithValue("@PhoneNumber", selectedClient.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", selectedClient.Address);
                        command.Parameters.AddWithValue("@ClientID", selectedClient.ClientID);

                        command.ExecuteNonQuery();
                    }
                }

                LoadClients(); // Refresh the DataGrid
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Client selectedClient)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "DELETE FROM clients WHERE ClientID = @ClientID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClientID", selectedClient.ClientID);
                        command.ExecuteNonQuery();
                    }
                }

                LoadClients(); // Refresh the DataGrid
            }
        }
    }
}
