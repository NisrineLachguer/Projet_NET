using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace WpfApp1.Views
{
    public partial class EmployeeControl : UserControl
    {
        string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
        private string selectedImagePath;

        public EmployeeControl()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(selectedImagePath));
                imgProfile.Source = bitmap;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validation for empty fields
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(cboRole.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) || imgProfile.Source == null)
            {
                MessageBox.Show("Please fill all fields and upload an image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate email format
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate phone format (simple numeric check, adjust as needed)
            if (!IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string imagePath = SaveImageToSpecifiedPath();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO employees (Name, Role, Email, Phone, Address, PhotoPath) VALUES (@Name, @Role, @Email, @Phone, @Address, @PhotoPath)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Role", cboRole.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@PhotoPath", imagePath);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            ClearForm();
            LoadEmployees();
            MessageBox.Show("Employee saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem is DataRowView selectedRow)
            {
                // Validation for empty fields
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(cboRole.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Please fill all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate email format
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate phone format (simple numeric check, adjust as needed)
                if (!IsValidPhone(txtPhone.Text))
                {
                    MessageBox.Show("Please enter a valid phone number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string imagePath = selectedImagePath != null ? SaveImageToSpecifiedPath() : selectedRow["PhotoPath"].ToString();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE employees SET Name = @Name, Role = @Role, Email = @Email, Phone = @Phone, Address = @Address, PhotoPath = @PhotoPath WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Role", cboRole.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@PhotoPath", imagePath);
                    cmd.Parameters.AddWithValue("@Id", selectedRow["Id"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                LoadEmployees();
                MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select an employee to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DeleteImage(string imagePath)
        {
            try
            {
                // Attempt to open the file exclusively to check if it's in use
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    // If this succeeds, it means no other process is using the file
                    fs.Close();
                }

                // Try deleting the image
                File.Delete(imagePath);
            }
            catch (IOException)
            {
                // If an IOException is thrown, the file might be in use, so wait a bit and try again
                MessageBox.Show("The image file is currently in use. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem is DataRowView selectedRow)
            {
                string imagePath = selectedRow["PhotoPath"].ToString();

                // Delete the employee's photo
                DeleteImage(imagePath);

                // Proceed with the deletion from the database
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM employees WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", selectedRow["Id"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                LoadEmployees();
                MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees(txtSearch.Text);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }

        private void DgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEmployees.SelectedItem is DataRowView selectedRow)
            {
                txtName.Text = selectedRow["Name"].ToString();
                cboRole.Text = selectedRow["Role"].ToString();
                txtEmail.Text = selectedRow["Email"].ToString();
                txtPhone.Text = selectedRow["Phone"].ToString();
                txtAddress.Text = selectedRow["Address"].ToString();

                if (selectedRow["PhotoPath"] != DBNull.Value && !string.IsNullOrEmpty(selectedRow["PhotoPath"].ToString()))
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(selectedRow["PhotoPath"].ToString()));
                        imgProfile.Source = bitmap;
                    }
                    catch (Exception)
                    {
                        imgProfile.Source = null;
                    }
                }
                else
                {
                    imgProfile.Source = null;
                }
            }
        }

        private void LoadEmployees(string searchTerm = "")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Role, Email, Phone, Address, PhotoPath FROM employees";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += " WHERE Name LIKE @SearchTerm";
                }
                query += " ORDER BY Id ASC";  // Ordering by Id in ascending order

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                }

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgEmployees.ItemsSource = dt.DefaultView;

                // Display number of employees
                lblEmployeeCount.Content = $"Total Employees: {dt.Rows.Count}";
            }
        }

        private string SaveImageToSpecifiedPath()
        {
            string directoryPath = @"C:\Users\irharissa.NPIF\Desktop\EMSI\.NET\Projet_NET\hotel_management_wpf\Gestion_hotel\Images\profileEmploye";
            Directory.CreateDirectory(directoryPath);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(selectedImagePath)}";
            string destinationPath = Path.Combine(directoryPath, fileName);

            File.Copy(selectedImagePath, destinationPath, true);

            return destinationPath;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            MessageBox.Show("Form cleared successfully.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearForm()
        {
            txtName.Clear();
            cboRole.SelectedIndex = -1;
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            imgProfile.Source = null;
            selectedImagePath = null;
            dgEmployees.SelectedItem = null;
        }

        // Email validation using regex
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Phone validation (adjust as necessary based on your requirements)
        private bool IsValidPhone(string phone)
        {
            return phone.All(char.IsDigit) && phone.Length >= 10 && phone.Length <= 15;  // Example for numeric and length validation
        }
    }
}
