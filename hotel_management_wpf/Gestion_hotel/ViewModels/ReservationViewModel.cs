using System.Collections.ObjectModel;
using System.Linq; // Pour utiliser FirstOrDefault()
using System.Windows;
using System.Windows.Input;
using WpfApp1.DataAccess.Repositories;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        // Collection de réservations
        public ObservableCollection<Reservation> Reservations { get; set; }

        // Commandes pour ajouter et supprimer des réservations
        public ICommand AddReservationCommand { get; set; }
        public ICommand DeleteReservationCommand { get; set; }

        private ReservationRepository _repository;

        // Constructeur de la classe
        public ReservationViewModel()
        {
            // Initialisation du repository et de la collection de réservations
            _repository = new ReservationRepository();
            Reservations = new ObservableCollection<Reservation>(_repository.GetAllReservations());

            // Initialisation des commandes
            AddReservationCommand = new RelayCommand(AddReservation);
            DeleteReservationCommand = new RelayCommand(DeleteReservation);
        }

        // Méthode pour supprimer une réservation
        private void DeleteReservation(object obj)
        {
            var reservationToDelete = obj as Reservation; // Vérification si c'est bien une réservation

            if (reservationToDelete != null)
            {
                // Suppression de la réservation en passant son ID
                int rowsAffected = _repository.DeleteReservation(reservationToDelete.Id);

                if (rowsAffected > 0)
                {
                    Reservations.Remove(reservationToDelete);
                    MessageBox.Show("Réservation supprimée avec succès !");
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression de la réservation.");
                }
            }
            else
            {
                MessageBox.Show("Aucune réservation à supprimer.");
            }
        }

        // Méthode pour ajouter une réservation
        private void AddReservation(object obj)
        {
            // Créer une nouvelle réservation, ici tu peux récupérer les valeurs des TextBox de l'UI par exemple
            var newReservation = new Reservation
            {
                ClientId = 1, // Tu peux récupérer ces informations depuis l'interface
                RoomId = 101,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                TotalCost = 200.50m,
                ReservationState = ReservationState.Pending
            };

            // Ajouter la réservation à la base de données
            int rowsAffected = _repository.AddReservation(newReservation);

            // Mettre à jour la collection de réservations
            if (rowsAffected > 0)
            {
                Reservations.Add(newReservation);
                MessageBox.Show("Réservation ajoutée avec succès !");
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout de la réservation.");
            }
        }
    }
}
