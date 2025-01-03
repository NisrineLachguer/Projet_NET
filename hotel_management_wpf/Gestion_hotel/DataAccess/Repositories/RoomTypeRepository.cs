using WpfApp1.Models;

namespace WpfApp1.DataAccess.Repositories;

using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class RoomTypeRepository
{
    public IEnumerable<RoomType> GetAllRoomTypes()
    {
        var roomTypes = new List<RoomType>();
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            var command = new MySqlCommand("SELECT * FROM RoomType", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    roomTypes.Add(new RoomType
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                        Price = reader.GetDecimal("Price"),
                        Capacity = reader.GetInt32("Capacity")
                    });
                }
            }
        }
        return roomTypes;
    }
}

