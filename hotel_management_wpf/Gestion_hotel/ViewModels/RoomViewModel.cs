using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfApp1.DataAccess.Repositories;
using WpfApp1.Models;

namespace WpfApp1.ViewModels;
public class RoomViewModel : BaseViewModel
    {
        public ObservableCollection<Room> Rooms { get; set; }

        public ICommand AddRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }

        private RoomRepository _repository;

        public RoomViewModel()
        {
            _repository = new RoomRepository();
            Rooms = new ObservableCollection<Room>(_repository.GetAllRooms());

          //  AddRoomCommand = new RelayCommand(AddRoom);
         //   DeleteRoomCommand = new RelayCommand(DeleteRoom);
        }

        private void AddRoom()
        {
            // Logic for adding a Room
            MessageBox.Show("Add Room functionality to be implemented!");
        }

        private void DeleteRoom()
        {
            // Logic for deleting a Room
            MessageBox.Show("Delete Room functionality to be implemented!");
        }
    }
