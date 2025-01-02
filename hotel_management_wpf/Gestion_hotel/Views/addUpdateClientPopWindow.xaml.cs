using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp1
{
    public partial class addUpdateClientPopWindow : Window
    {
        public addUpdateClientPopWindow(string name = "", string email = "", string phoneNumber = "", string address = "")
        {
            InitializeComponent();

            // Populate fields with initial values
            NameTextBox.Text = name;
            EmailTextBox.Text = email;
            PhoneNumberTextBox.Text = phoneNumber;
            AddressTextBox.Text = address;

            // Disable the Save button initially
            SaveButton.IsEnabled = false;

            // Attach event handlers to inputs
            NameTextBox.TextChanged += ValidateInputs;
            EmailTextBox.TextChanged += ValidateInputs;
            PhoneNumberTextBox.TextChanged += ValidateInputs;
            AddressTextBox.TextChanged += ValidateInputs;
        }

        private void ValidateInputs(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Validate that all fields are filled
            bool allFieldsFilled = !string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                                   !string.IsNullOrWhiteSpace(EmailTextBox.Text) &&
                                   !string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) &&
                                   !string.IsNullOrWhiteSpace(AddressTextBox.Text);

            // Validate email format
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool validEmail = Regex.IsMatch(EmailTextBox.Text, emailPattern);

            // Enable the Save button only if all validations pass
            SaveButton.IsEnabled = allFieldsFilled && validEmail;

            // Provide feedback to the user if the email is invalid
            if (!validEmail && !string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailValidationMessage.Visibility = Visibility.Visible;
                EmailValidationMessage.Text = "Invalid email format.";
            }
            else
            {
                EmailValidationMessage.Visibility = Visibility.Hidden;
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
