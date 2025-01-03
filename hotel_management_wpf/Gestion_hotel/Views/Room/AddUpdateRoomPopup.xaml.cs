using System.Text.RegularExpressions;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class AddUpdateRoomWindow : Window
    {
        public AddUpdateRoomWindow(List<RoomType> roomType, RoomType selectedRoomType = null, string roomNumber = "", bool availability = false, string description = "")
        {
            InitializeComponent();
            // Populate RoomType ComboBox
            RoomTypeComboBox.ItemsSource = roomType;
            RoomTypeComboBox.SelectedItem = selectedRoomType;

            // Populate fields with initial values

            RoomNumberTextBox.Text = roomNumber;
            AvailabilityCheckBox.IsChecked = availability;
            DescriptionTextBox.Text = description;

            // Disable the Save button initially
            SaveButton.IsEnabled = false;

            // Attach event handlers to inputs
            RoomTypeComboBox.SelectionChanged += ValidateInputs;
            RoomNumberTextBox.TextChanged += ValidateInputs;
            DescriptionTextBox.TextChanged += ValidateInputs;
        }

        private void ValidateInputs(object sender, object e)
        {
            // Validate that all fields are filled
            bool allFieldsFilled = RoomTypeComboBox.SelectedItem != null &&
                                   !string.IsNullOrWhiteSpace(RoomNumberTextBox.Text) &&
                                   !string.IsNullOrWhiteSpace(DescriptionTextBox.Text);

            // Validate room number format
            bool validRoomNumber = int.TryParse(RoomNumberTextBox.Text, out _);

            // Enable the Save button only if all validations pass
            SaveButton.IsEnabled = allFieldsFilled && validRoomNumber;

            // Provide feedback to the user if the room number is invalid
            if (!validRoomNumber && !string.IsNullOrWhiteSpace(RoomNumberTextBox.Text))
            {
                RoomNumberValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                RoomNumberValidationMessage.Visibility = Visibility.Hidden;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup and set DialogResult to true
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup and set DialogResult to false
            this.DialogResult = false;
        }
    }
}