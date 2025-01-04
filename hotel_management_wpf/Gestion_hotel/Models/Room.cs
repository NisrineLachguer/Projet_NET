namespace WpfApp1.Models;

public class Room
{
    public int Id { get; set; }
    public int RoomTypeId { get; set; }
    public int RoomNumber { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; }
}
