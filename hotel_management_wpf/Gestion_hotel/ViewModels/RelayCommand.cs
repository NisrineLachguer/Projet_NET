using System.Windows.Input;

namespace WpfApp1.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute; // Action à exécuter
    private readonly Func<object, bool> _canExecute; // Fonction qui détermine si la commande peut être exécutée

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // Détermine si la commande peut être exécutée
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    // Événement qui notifie le changement de l'état d'exécution
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    // Exécute l'action
    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}