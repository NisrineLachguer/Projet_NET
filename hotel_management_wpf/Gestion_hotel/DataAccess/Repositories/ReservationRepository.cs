using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using WpfApp1.Models;

namespace WpfApp1.DataAccess.Repositories
{
    public class ReservationRepository
    {
        // Obtenir toutes les réservations
        public List<Reservation> GetAllReservations()
        {
            string query = "SELECT ReservationID, ClientID, RoomID, StartDate, EndDate, TotalCost, ReservationState FROM Reservations";
            var table = DatabaseHelper.ExecuteQuery(query);
            var reservations = new List<Reservation>();

            foreach (DataRow row in table.Rows)
            {
                reservations.Add(new Reservation
                {
                    Id = row["ReservationID"] != DBNull.Value ? int.Parse(row["ReservationID"].ToString()) : 0,
                    Client = row["ClientID"] != DBNull.Value ? GetClientById(int.Parse(row["ClientID"].ToString())) : null,

                    Room = GetRoomById(row["RoomID"] != DBNull.Value ? int.Parse(row["RoomID"].ToString()) : 0),
                    StartDate = DateTime.TryParse(row["StartDate"]?.ToString(), out var startDate) ? startDate : DateTime.MinValue,
                    EndDate = DateTime.TryParse(row["EndDate"]?.ToString(), out var endDate) ? endDate : DateTime.MinValue,
                    TotalCost = decimal.TryParse(row["TotalCost"]?.ToString(), out var totalCost) ? totalCost : 0m,
                    ReservationState = Enum.TryParse(row["ReservationState"]?.ToString(), out ReservationState state) ? state : ReservationState.Default
                });
            }

            return reservations;
        }

        // Obtenir une réservation par ID
        public Reservation GetReservationById(int id)
        {
            string query = "SELECT * FROM Reservations WHERE ReservationID = @id";
            var parameters = new[] { new MySqlParameter("@id", id) };
            var table = DatabaseHelper.ExecuteQuery(query, parameters);

            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                return new Reservation
                {
                    Id = row["ReservationID"] != DBNull.Value ? int.Parse(row["ReservationID"].ToString()) : 0,
                    Client = row["ClientID"] != DBNull.Value ? GetClientById(int.Parse(row["ClientID"].ToString())) : null,
                    Room = GetRoomById(int.TryParse(row["RoomID"]?.ToString(), out var roomId) ? roomId : 0),
                    StartDate = DateTime.TryParse(row["StartDate"]?.ToString(), out var startDate) ? startDate : DateTime.MinValue,
                    EndDate = DateTime.TryParse(row["EndDate"]?.ToString(), out var endDate) ? endDate : DateTime.MinValue,
                    TotalCost = decimal.TryParse(row["TotalCost"]?.ToString(), out var totalCost) ? totalCost : 0m,
                    ReservationState = Enum.TryParse(row["ReservationState"]?.ToString(), out ReservationState state) ? state : ReservationState.Default
                };
            }
            return null;
        }

        // Ajouter une nouvelle réservation
        public int AddReservation(Reservation reservation)
        {
            string query = "INSERT INTO Reservations (ClientID, RoomID, StartDate, EndDate, TotalCost, ReservationState) " +
                           "VALUES (@ClientID, @RoomID, @StartDate, @EndDate, @TotalCost, @ReservationState)";

            var parameters = new[]
            {
                new MySqlParameter("@ClientID", reservation.Client.ClientID),
                new MySqlParameter("@RoomID", reservation.Room.RoomID),
                new MySqlParameter("@StartDate", reservation.StartDate),
                new MySqlParameter("@EndDate", reservation.EndDate),
                new MySqlParameter("@TotalCost", reservation.TotalCost),
                new MySqlParameter("@ReservationState", reservation.ReservationState.ToString())
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Mettre à jour une réservation existante
        public void UpdateReservation(Reservation reservation)
        {
            string query = "UPDATE Reservations SET ClientID = @ClientID, RoomID = @RoomID, StartDate = @StartDate, " +
                           "EndDate = @EndDate, TotalCost = @TotalCost, ReservationState = @ReservationState " +
                           "WHERE ReservationID = @ReservationID";

            var parameters = new[]
            {
                new MySqlParameter("@ClientID", reservation.Client.ClientID),
                new MySqlParameter("@RoomID", reservation.Room.RoomID),
                new MySqlParameter("@StartDate", reservation.StartDate),
                new MySqlParameter("@EndDate", reservation.EndDate),
                new MySqlParameter("@TotalCost", reservation.TotalCost),
                new MySqlParameter("@ReservationState", reservation.ReservationState.ToString()),
                new MySqlParameter("@ReservationID", reservation.Id)
            };

            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Supprimer une réservation
        public int DeleteReservation(int id)
        {
            string query = "DELETE FROM Reservations WHERE ReservationID = @id";
            var parameters = new[] { new MySqlParameter("@id", id) };

            // Exécute la requête et récupère le nombre de lignes affectées
            return DatabaseHelper.ExecuteNonQuery(query, parameters); // Retourner le nombre de lignes affectées
        }

        // Récupérer un client par ID
        private Client GetClientById(int clientId)
        {
            string query = "SELECT * FROM Clients WHERE ClientID = @clientId";
            var parameters = new[] { new MySqlParameter("@clientId", clientId) };
            var table = DatabaseHelper.ExecuteQuery(query, parameters);

            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                return new Client
                {
                    ClientID = int.Parse(row["ClientID"].ToString()),
                    Name = row["ClientName"]?.ToString(),
                    Address = row["Address"]?.ToString(),
                    PhoneNumber = row["PhoneNumber"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    
                };
            }
            return null;
        }

        // Récupérer une chambre par ID
        private Room GetRoomById(int roomId)
        {
            string query = "SELECT * FROM Rooms WHERE RoomID = @roomId";
            var parameters = new[] { new MySqlParameter("@roomId", roomId) };
            var table = DatabaseHelper.ExecuteQuery(query, parameters);

            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                return new Room
                {
                    RoomID = int.Parse(row["RoomID"].ToString()),
                    RoomType = row["RoomType"]?.ToString(),
                    Capacity = int.Parse(row["Capacity"]?.ToString()),
                    PricePerNight = decimal.TryParse(row["PricePerNight"]?.ToString(), out var price) ? price : 0m,
                    IsAvailable = bool.TryParse(row["IsAvailable"]?.ToString(), out var isAvailable) && isAvailable,
                    Description = row["Description"]?.ToString()
                };
            }
            return null;
        }
    }
}
