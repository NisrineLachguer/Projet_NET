namespace WpfApp1.Models;

public class Reservation
{
    public Reservation(int id, Client client, Room room, DateTime startDate, DateTime endDate, decimal totalCost, ReservationState reservationState)
    {
        Id = id;
        Client = client;
        Room = room;
        StartDate = startDate;
        EndDate = endDate;
        TotalCost = totalCost;
        ReservationState = reservationState;
    }

    public Reservation()
    {
        throw new NotImplementedException();
    }

    public int Id { get; set; }
    public Client Client { get; set; }
    public Room Room { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalCost { get; set; }
    
    public ReservationState ReservationState { get; set; }
    public int ClientId { get; set; }
    public int RoomId { get; set; }
    public object Name { get; set; }
}
