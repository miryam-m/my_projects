
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Entities;
using DAL;

namespace BL
{
    public class Passengers_BL
    {
        private readonly Passengers_DAL PassengersDAL;

        public Passengers_BL(Passengers_DAL passDAL)
        {
            PassengersDAL = passDAL;
        }

        // המרת נוסע ל-DTO
        public Passengers_DTO ConvertToDTO(PassengersTbl pass)
        {
            return new Passengers_DTO
            {
                Id = pass.Id,
                FirstName = pass.FirstName,
                LastName = pass.LastName,
                City = pass.City,
                Adress = pass.Adress,
                Phone = pass.Phone,
                Email = pass.Email
            };
        }

        // המרת DTO לנוסע
        public PassengersTbl ConvertFromDTO(Passengers_DTO dto)
        {
            return new PassengersTbl
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                City = dto.City,
                Adress = dto.Adress,
                Phone = dto.Phone,
                Email = dto.Email
            };
        }

        // הוספת נוסע
        public async Task AddPassengerAsync(Passengers_DTO passengerDTO)
        {
            var passenger = ConvertFromDTO(passengerDTO);
            await PassengersDAL.AddPassenger(passenger);
        }

        // עדכון נוסע
        public async Task UpdatePassengerAsync(Passengers_DTO passengerDTO)
        {
            var passenger = ConvertFromDTO(passengerDTO);
            await PassengersDAL.UpdatePassengerAsync(passenger);
        }

        // מחיקת נוסע
        public async Task DeletePassengerAsync(int passengerId)
        {
            await PassengersDAL.DeletePassengerAsync(passengerId);
        }

        // קבלת נוסע לפי ID
        public async Task<Passengers_DTO?> GetPassengerAsync(int id)
        {
            var passenger = await PassengersDAL.SelectPassengerByIdAsync(id);
            return ConvertToDTO(passenger);
        }

        // קבלת כל הנוסעים
        public async Task<List<Passengers_DTO>> GetAllPassengersAsync()
        {
            var passengers = await PassengersDAL.SelectAllAsync();
            return passengers.Select(p => ConvertToDTO(p)).ToList();
        }
    }
}