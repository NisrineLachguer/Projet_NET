using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace WpfApp1.mail_pdf;

public class PdfGenerator
{
    private static string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";

    // Generate PDF dynamically from the employee and client data
    public static byte[] GeneratePdf()
    {
        using (var memoryStream = new MemoryStream())
        {
            var document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, memoryStream);
            document.Open();    
            document.Add(new Paragraph("Employee and Client List"));

            // Fetch employees and clients data
            var employeeData = GetEmployees();
            var clientData = GetClients();

            // Add Employees Data to PDF
            document.Add(new Paragraph("Employees:"));
            foreach (var employee in employeeData)
            {
                document.Add(new Paragraph($"Name: {employee.Name}, Role: {employee.Role}, Email: {employee.Email}, Phone: {employee.Phone}"));
            }

            // Add Clients Data to PDF
            document.Add(new Paragraph("Clients:"));
            foreach (var client in clientData)
            {
                document.Add(new Paragraph($"Name: {client.Name}, Email: {client.Email}, Phone: {client.Phone}, Address: {client.Address}, Created At: {client.CreatedAt}"));
            }

            document.Close();
            return memoryStream.ToArray();
        }
    }

    // Get employee data from MySQL
    private static List<Employee> GetEmployees()
    {
        var employees = new List<Employee>();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var query = "SELECT name, role, email, phone FROM employees";
            var cmd = new MySqlCommand(query, connection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Name = reader.GetString("name"),
                        Role = reader.GetString("role"),
                        Email = reader.GetString("email"),
                        Phone = reader.GetString("phone")
                    });
                }
            }
        }
        return employees;
    }

    // Get client data from MySQL
    private static List<Client> GetClients()
    {
        var clients = new List<Client>();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var query = "SELECT name, email, phone, address, created_at FROM clients";
            var cmd = new MySqlCommand(query, connection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    clients.Add(new Client
                    {
                        Name = reader.GetString("name"),
                        Email = reader.GetString("email"),
                        Phone = reader.GetString("phone"),
                        Address = reader.GetString("address"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
        }
        return clients;
    }
}