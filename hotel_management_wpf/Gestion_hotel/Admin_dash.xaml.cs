using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WpfApp1.Views;

namespace WpfApp1;

public partial class Admin_dash : Window
{
    public Admin_dash()
    {
        InitializeComponent();
        this.Loaded += Admin_dash_Loaded;
    }
    
    private void Admin_dash_Loaded(object sender, RoutedEventArgs e)
    {
        // Find and select the Dashboard item
        if (SidebarItems != null)
        {
            foreach (ListViewItem item in SidebarItems.Items)
            {
                if (item.Name == "Dashboard")
                {
                    item.IsSelected = true;
                    break;
                }
            }
        }
    }
    
    private void btnExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }

    private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UserControl usc = null;
        ContentMain.Children.Clear();

        var selectedItem = ((ListView)sender).SelectedItem as ListViewItem;
        if (selectedItem != null)
        {
            switch (selectedItem.Name)
            {
                case "ItemClients":
                    usc = new ClientsControl();
                    break;
                case "ItemEmployees":
                    usc = new EmployeeControl();
                    break;
                case "ItemSales":
                    usc = new BookingControl();
                    break;
                case "Dashboard":
                    usc = new DashboardControl();
                    break;
                case "ItemRoom":
                    usc = new RoomControl();
                    break;
                case "ItemCate":
                    usc = new RoomTypeControl();
                    break;
                case "ItemBooking":
                    usc = new ReservationControl();
                    break;
            }

            if (usc != null)
            {
                ContentMain.Children.Add(usc);
            }
        }
    }
}