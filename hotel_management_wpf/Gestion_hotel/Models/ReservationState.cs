namespace WpfApp1.Models;

public enum ReservationState
{
    Default,
    Pending,     // Réservation en attente
    Confirmed,   // Réservation confirmée
    Cancelled,   // Réservation annulée
    CheckedIn,   // Client a enregistré son entrée
    CheckedOut   // Client a quitté l'hôtel
}