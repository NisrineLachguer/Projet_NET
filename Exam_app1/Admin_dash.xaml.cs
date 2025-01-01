using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Exam_app1
{
    /// <summary>
    /// Logique d'interaction pour Admin_dash.xaml
    /// </summary>
    public partial class Admin_dash : Window
    {
        public Admin_dash()
        {
            InitializeComponent();
        }

        private void RoomsButton_Click(object sender, RoutedEventArgs e)
        {
         //   Rooms_view rooms = new Rooms_view();
          //  rooms.Show();
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
           // Clients_view clients = new Clients_view();
           // clients.Show();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
           // Employes_view employes = new Employes_view();
          //  employes.Show();
        }

        private void BookingsButton_Click(object sender, RoutedEventArgs e)
        {
           // Booking booking = new Booking();
           // booking.Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
           // setting param = new setting();
           // param.Show();

        }
    }

}
}
