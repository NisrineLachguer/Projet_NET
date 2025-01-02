using System.Collections.ObjectModel;
using WpfApp1.DataAccess.Repositories;
using WpfApp1.Models;

namespace WpfApp1.ViewModels;

public class ClientViewModel : BaseViewModel
{
    private readonly ClientRepository _clientRepository;
    public ObservableCollection<Client> Clients { get; set; }

    public ClientViewModel()
    {
        _clientRepository = new ClientRepository();
        Clients = new ObservableCollection<Client>(_clientRepository.GetAllClients());
    }

    public void AddClient(Client client)
    {
        _clientRepository.AddClient(client);
        Clients.Add(client);
    }

    public void UpdateClient(Client client)
    {
        _clientRepository.UpdateClient(client);
        // Mettre à jour la liste en mémoire si nécessaire
    }

    public void DeleteClient(Client client)
    {
        _clientRepository.DeleteClient(client.ClientID);
        Clients.Remove(client);
    }
}