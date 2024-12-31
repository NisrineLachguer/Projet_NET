using System.Windows;
using WpfApp1.Models;

namespace WpfApp1;

public partial class Admin_dash : Window
{
    public Admin_dash()
    {
        InitializeComponent();
    }
    private void RoomsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new RoomControl();
    }
    
    private void RoomCategoriesButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new RoomTypeControl();
    }
    private void ClientsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new ClientsControl();

    }

    private void EmployeesButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new EmployeesControl();
    }

    private void BookingsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new BookingControl();
    }

    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new SettingsControl();
    }
}