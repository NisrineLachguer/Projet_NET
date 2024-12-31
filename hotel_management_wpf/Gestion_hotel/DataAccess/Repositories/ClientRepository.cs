using System.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using WpfApp1.Models;
using Client = WpfApp1.Models.Client;

namespace WpfApp1.DataAccess.Repositories;

public class ClientRepository
{
    
      public List<Client> GetAllClients()
        {
            string query = "SELECT * FROM clients";
            var table = DatabaseHelper.ExecuteQuery(query);
            var clients = new List<Client>();

            foreach (DataRow row in table.Rows)
            {
                clients.Add(new Client
                {
                    ClientID = int.Parse(row["ClientID"].ToString()),
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    Address = row["Address"].ToString()
                });
            }

            return clients;
        }

        public Client GetClientById(int id)
        {
            string query = "SELECT * FROM clients WHERE ClientID = @id";
            var parameters = new[] { new MySqlParameter("@id", id) };
            var table = DatabaseHelper.ExecuteQuery(query, parameters);

            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                return new Client
                {
                    ClientID = int.Parse(row["ClientID"].ToString()),
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    Address = row["Address"].ToString()
                };
            }
            return null;
        }

        public void AddClient(Client client)
        {
            string query = "INSERT INTO clients (Name, Email, PhoneNumber, Address) VALUES (@name, @email, @phone, @address)";
            var parameters = new[]
            {
                new MySqlParameter("@name", client.Name),
                new MySqlParameter("@email", client.Email),
                new MySqlParameter("@phone", client.PhoneNumber),
                new MySqlParameter("@address", client.Address)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public void UpdateClient(Client client)
        {
            string query = "UPDATE clients SET Name = @name, Email = @email, PhoneNumber = @phone, Address = @address WHERE ClientID = @id";
            var parameters = new[]
            {
                new MySqlParameter("@name", client.Name),
                new MySqlParameter("@email", client.Email),
                new MySqlParameter("@phone", client.PhoneNumber),
                new MySqlParameter("@address", client.Address),
                new MySqlParameter("@id", client.ClientID)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public void DeleteClient(int id)
        {
            string query = "DELETE FROM clients WHERE ClientID = @id";
            var parameters = new[] { new MySqlParameter("@id", id) };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
}