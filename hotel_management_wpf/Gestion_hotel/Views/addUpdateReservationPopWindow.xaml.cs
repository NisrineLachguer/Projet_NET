using System.Windows;

namespace WpfApp1
{
    public partial class AddUpdateReservationPopWindow : Window
    {
        public AddUpdateReservationPopWindow()
        {
            InitializeComponent();
        }

        public AddUpdateReservationPopWindow(string? toString, string? s, string? toString1)
        {
            throw new NotImplementedException();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}