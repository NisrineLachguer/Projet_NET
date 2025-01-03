namespace WpfApp1.Models;

public class Room
{
    public int Id { get; set; }
    public int RoomTypeId { get; set; }
    public int RoomNumber { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; }
    public int RoomID { get; set; }
    public string? RoomType { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
}
