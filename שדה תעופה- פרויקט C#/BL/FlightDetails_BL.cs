
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
    public class FlightDetails_BL
    {
        private readonly FlightDetails_DAL FlightdetDAL;

        public FlightDetails_BL(FlightDetails_DAL flydetDAL)
        {
            FlightdetDAL = flydetDAL;
        }

        public FlightDetails_DTO? ConvertToDTO(FlightDetailsTbl flydet)
        {
            if (flydet == null) return null;

            FlightDetails_DTO dto = new FlightDetails_DTO
            {
                FlightDetailsId = flydet.FlightDetailsId,
                FlightId = flydet.FlightId,
                FlightTime = flydet.FlightTime,
                NumOfPassengers = flydet.NumOfPassengers,
                NumOfAvailableSeats = flydet.NumOfAvailableSeats,
                Price = flydet.Price
            };
            return dto;
        }

        public FlightDetailsTbl? ConvertFromDTO(FlightDetails_DTO dto)
        {
            if (dto == null) return null;

            return new FlightDetailsTbl
            {
                FlightDetailsId = dto.FlightDetailsId,
                FlightId = dto.FlightId,
                FlightTime = dto.FlightTime,
                NumOfPassengers = dto.NumOfPassengers,
                NumOfAvailableSeats = dto.NumOfAvailableSeats,
                Price = dto.Price
            };
        }

        // פונקציה להוספת פרטי טיסה בעזרת DTO
        public async Task AddFlightDetailAsync(FlightDetails_DTO flightDetailDto)
        {
            var flightDetail = ConvertFromDTO(flightDetailDto);
#pragma warning disable CS8604 // Possible null reference argument.
            await FlightdetDAL.AddFlightDetailAsync(flightDetail);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // פונקציה לעדכון פרטי טיסה בעזרת DTO
        public async Task UpdateFlightDetailAsync(FlightDetails_DTO flightDetailDto)
        {
            var flightDetail = ConvertFromDTO(flightDetailDto);
#pragma warning disable CS8604 // Possible null reference argument.
            await FlightdetDAL.UpdateFlightDetailAsync(flightDetail);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // פונקציה למחיקת פרטי טיסה בעזרת DTO
        public async Task DeleteFlightDetailAsync(int flightDetailId)
        {
            await FlightdetDAL.DeleteFlightDetailAsync(flightDetailId);
        }

        // פונקציה לשליפת פרטי טיסה לפי ID בעזרת DTO
        public async Task<FlightDetails_DTO?> GetFlightDetailAsync(int flightDetailId)
        {
            var flightDetail = await FlightdetDAL.GetFlightDetailAsync(flightDetailId);
#pragma warning disable CS8604 // Possible null reference argument.
            return ConvertToDTO(flightDetail);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // פונקציה לשליפת כל פרטי הטיסה בעזרת DTO
        public async Task<List<FlightDetails_DTO>> GetAllFlightDetailsAsync()
        {
            var flightDetails = await FlightdetDAL.GetAllFlightDetailsAsync();
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return flightDetails.Select(fd => ConvertToDTO(fd)).ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        // פונקציה לשליפת פרטי טיסה לפי טיסה בעזרת DTO
        public async Task<List<FlightDetails_DTO>> GetFlightDetailsByFlightIdAsync(int flightId)
        {
            var flightDetails = await FlightdetDAL.GetFlightDetailsByFlightIdAsync(flightId);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return flightDetails.Select(fd => ConvertToDTO(fd)).ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}