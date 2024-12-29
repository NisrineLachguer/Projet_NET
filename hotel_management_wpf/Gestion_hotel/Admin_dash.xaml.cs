using System.Windows;
namespace WpfApp1;

public partial class Admin_dash : Window
{
    public Admin_dash()
    {
        InitializeComponent();
    }
    private void RoomsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new RoomsControl();
    }

    private void ClientsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new EmployeesControl();
    }

    private void EmployeesButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new ClientsControl();
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