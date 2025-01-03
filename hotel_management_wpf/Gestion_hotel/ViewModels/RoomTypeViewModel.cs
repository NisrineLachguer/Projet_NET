using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfApp1.DataAccess.Repositories;
using WpfApp1.Models;

namespace WpfApp1.ViewModels;

public class RoomTypeViewModel : BaseViewModel
{
    public ObservableCollection<RoomType> RoomTypes { get; set; }

    public ICommand AddRoomTypeCommand { get; set; }
    public ICommand DeleteRoomTypeCommand { get; set; }

    private RoomTypeRepository _repository;

    public RoomTypeViewModel()
    {
        _repository = new RoomTypeRepository();
        RoomTypes = new ObservableCollection<RoomType>(_repository.GetAllRoomTypes());

     //   AddRoomTypeCommand = new RelayCommand(AddRoomType);
       // DeleteRoomTypeCommand = new RelayCommand(DeleteRoomType);
    }

    private void AddRoomType()
    {
        // Logic for adding a RoomType
        MessageBox.Show("Add RoomType functionality to be implemented!");
    }

    private void DeleteRoomType()
    {
        // Logic for deleting a RoomType
        MessageBox.Show("Delete RoomType functionality to be implemented!");
    }
}