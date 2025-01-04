using System.Windows;
using System.Windows.Controls;

public class InputDialog : Window
{
    private TextBox textBox;
    
    public string ResponseText
    {
        get { return textBox.Text; }
    }

    public InputDialog(string question)
    {
        Width = 300;
        Height = 150;
        Title = "Email Recipient";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        
        var label = new Label { Content = question, Margin = new Thickness(5) };
        grid.Children.Add(label);
        Grid.SetRow(label, 0);
        
        var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        textBox = new TextBox { Margin = new Thickness(5), Width = 200 };
        var okButton = new Button { Content = "OK", Width = 60, Margin = new Thickness(5) };
        var cancelButton = new Button { Content = "Cancel", Width = 60, Margin = new Thickness(5) };
        
        okButton.Click += (s, e) => { DialogResult = true; };
        cancelButton.Click += (s, e) => { DialogResult = false; };
        
        buttonPanel.Children.Add(textBox);
        buttonPanel.Children.Add(okButton);
        buttonPanel.Children.Add(cancelButton);
        
        grid.Children.Add(buttonPanel);
        Grid.SetRow(buttonPanel, 1);
        
        Content = grid;
    }
}