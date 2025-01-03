using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
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
        // Using Dispatcher to call ListViewMenu_SelectionChanged after the layout is fully rendered
        Dispatcher.BeginInvoke(new Action(() =>
        {
            if (SidebarItems != null)
            {
                ListViewMenu_SelectionChanged(SidebarItems, null);
            }
        }), System.Windows.Threading.DispatcherPriority.ContextIdle);
    }
    /*
    private void RoomsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentAreaControl.Content = new RoomControl();
    }*/
    
    
    /* private void ToggleMenu(object sender, RoutedEventArgs e)
    {
        // Toggle the sidebar width between expanded and collapsed
        if (Sidebar.Width == new GridLength(1, GridUnitType.Star))
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            var animation = new GridLengthAnimation
            {
                Duration = duration,
                From = new GridLength(1, GridUnitType.Star),
                To = new GridLength(0, GridUnitType.Star)
            };
            Sidebar.BeginAnimation(ColumnDefinition.WidthProperty, animation);
        }
        else
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            var animation = new GridLengthAnimation
            {
                Duration = duration,
                From = new GridLength(0, GridUnitType.Star),
                To = new GridLength(1, GridUnitType.Star)
            };
            Sidebar.BeginAnimation(ColumnDefinition.WidthProperty, animation);
        }
    }*/

    private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UserControl usc = null;
        ContentMain.Children.Clear();

        var selectedItem = ((ListView)sender).SelectedItem as ListViewItem;
        if (selectedItem != null)
        {
            // Change user control based on the selected menu item
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
                // Add more cases as necessary
            }

            if (usc != null)
            {
                ContentMain.Children.Add(usc);
            }
        }
    }

}