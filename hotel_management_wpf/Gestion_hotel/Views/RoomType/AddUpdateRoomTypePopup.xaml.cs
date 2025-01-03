using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp1
{
    public partial class AddUpdateRoomTypePopup : Window
    {
        public AddUpdateRoomTypePopup(string name = "", string description = "", string price = "", string capacity = "")
        {
            InitializeComponent();

            // Populate fields with initial values
            NameTextBox.Text = name;
            DescriptionTextBox.Text = description;
            PriceTextBox.Text = price;
            CapacityTextBox.Text = capacity;

            // Disable the Save button initially
            SaveButton.IsEnabled = false;

            // Attach event handlers to inputs
            NameTextBox.TextChanged += ValidateInputs;
            DescriptionTextBox.TextChanged += ValidateInputs;
            PriceTextBox.TextChanged += ValidateInputs;
            CapacityTextBox.TextChanged += ValidateInputs;
        }

        private void ValidateInputs(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate that all fields are filled
            bool allFieldsFilled = !string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                                   !string.IsNullOrWhiteSpace(DescriptionTextBox.Text) &&
                                   !string.IsNullOrWhiteSpace(PriceTextBox.Text) &&
                                   !string.IsNullOrWhiteSpace(CapacityTextBox.Text);

            // Validate price format
            bool validPrice = decimal.TryParse(PriceTextBox.Text, out _);

            // Validate capacity format
            bool validCapacity = int.TryParse(CapacityTextBox.Text, out _);

            // Enable the Save button only if all validations pass
            SaveButton.IsEnabled = allFieldsFilled && validPrice && validCapacity;

            // Provide feedback to the user for invalid price
            PriceValidationMessage.Visibility = validPrice || string.IsNullOrWhiteSpace(PriceTextBox.Text) ? Visibility.Hidden : Visibility.Visible;

            // Provide feedback to the user for invalid capacity
            CapacityValidationMessage.Visibility = validCapacity || string.IsNullOrWhiteSpace(CapacityTextBox.Text) ? Visibility.Hidden : Visibility.Visible;
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
