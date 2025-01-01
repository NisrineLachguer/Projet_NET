using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using MySql.Data.MySqlClient; // Add this

namespace WpfApp1.Views;

public partial class EmployeeControl : UserControl
{
    string connectionString = "server=localhost;port=3306;user=root;password=;database=hotel_management_wpf;";
    private readonly string photoFolder;
    private string currentPhotoPath;
    private int selectedEmployeeId = 0;
    
    public EmployeeControl()
    {
        InitializeComponent();
        photoFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "profileEmploye");
        Directory.CreateDirectory(photoFolder);
        LoadEmployees();
    }
     private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                currentPhotoPath = dialog.FileName;
                imgDisplay.Source = new BitmapImage(new Uri(currentPhotoPath));
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string photoPath = SavePhotoToFolder();
                using var conn = new MySqlConnection(connectionString);
                conn.Open();
                
                var cmd = new MySqlCommand("INSERT INTO employee (name, role, email, phone, address, photo_path) VALUES (@name, @role, @email, @phone, @address, @photo)", conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@role", cboRole.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@photo", photoPath);
                
                cmd.ExecuteNonQuery();
                LoadEmployees();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployeeId == 0) return;

            try
            {
                string photoPath = SavePhotoToFolder();
                using var conn = new MySqlConnection(connectionString);
                conn.Open();
                
                var cmd = new MySqlCommand("UPDATE employee SET name=@name, role=@role, email=@email, phone=@phone, address=@address, photo_path=@photo WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", selectedEmployeeId);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@role", cboRole.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@photo", photoPath);
                
                cmd.ExecuteNonQuery();
                LoadEmployees();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployeeId == 0) return;

            if (MessageBox.Show("Delete this employee?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using var conn = new MySqlConnection(connectionString);
                    conn.Open();
                    var cmd = new MySqlCommand("DELETE FROM employee WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedEmployeeId);
                    cmd.ExecuteNonQuery();
                    LoadEmployees();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM employee WHERE name LIKE @search OR role LIKE @search", conn);
                cmd.Parameters.AddWithValue("@search", $"%{txtSearch.Text}%");
                
                var adapter = new MySqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);
                dgEmployees.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSortName_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees("ORDER BY name");
        }

        private void BtnSortRole_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees("ORDER BY role");
        }

        private void DgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEmployees.SelectedItem is DataRowView row)
            {
                selectedEmployeeId = Convert.ToInt32(row["id"]);
                txtName.Text = row["name"].ToString();
                cboRole.Text = row["role"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtPhone.Text = row["phone"].ToString();
                txtAddress.Text = row["address"].ToString();
                
                string photoPath = row["photo_path"].ToString();
                if (File.Exists(photoPath))
                {
                    imgDisplay.Source = new BitmapImage(new Uri(photoPath));
                }
            }
        }

        private void LoadEmployees(string orderBy = "")
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();
                var cmd = new MySqlCommand($"SELECT * FROM employee {orderBy}", conn);
                var adapter = new MySqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);
                dgEmployees.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string SavePhotoToFolder()
        {
            if (string.IsNullOrEmpty(currentPhotoPath)) return "";
            
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(currentPhotoPath)}";
            string destPath = Path.Combine(photoFolder, fileName);
            File.Copy(currentPhotoPath, destPath, true);
            return destPath;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Clear();
            cboRole.Text = "";
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            imgDisplay.Source = null;
            currentPhotoPath = null;
            selectedEmployeeId = 0;
        }
    
}