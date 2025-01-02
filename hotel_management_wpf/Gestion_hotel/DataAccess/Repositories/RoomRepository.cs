using WpfApp1.Models;

namespace WpfApp1.DataAccess.Repositories;

using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class RoomRepository
{
    public IEnumerable<Room> GetAllRooms()
    {
        var rooms = new List<Room>();
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            var command = new MySqlCommand("SELECT * FROM Room", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rooms.Add(new Room
                    {
                        Id = reader.GetInt32("Id"),
                        RoomTypeId = reader.GetInt32("RoomTypeId"),
                        RoomNumber = reader.GetInt32("RoomNumber"),
                        IsAvailable = reader.GetBoolean("IsAvailable"),
                        Description = reader.GetString("Description")
                    });
                }
            }
        }
        return rooms;
    }

    public void AddRoom(Room room)
    {
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            var command = new MySqlCommand(
                "INSERT INTO Room (RoomTypeId, RoomNumber, IsAvailable, Description) VALUES (@RoomTypeId, @RoomNumber, @IsAvailable, @Description)",
                connection);
            command.Parameters.AddWithValue("@RoomTypeId", room.RoomTypeId);
            command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
            command.Parameters.AddWithValue("@IsAvailable", room.IsAvailable);
            command.Parameters.AddWithValue("@Description", room.Description);
            command.ExecuteNonQuery();
        }
    }
}
