
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.models;
using DAL;
using Entities;

namespace BL
{
    public class FlightBl
    {
        private readonly Flight_DAL flightDal;

        public FlightBl(Flight_DAL _flightDal)
        {
            flightDal = _flightDal;
        }

        // פונקציה אסינכרונית לשליפת כל הטיסות
        public async Task<List<Flight_DTO>> GetAllFlightsAsync()
        {
            var flights = await flightDal.SelectAllAsync();
            return flights.Select(f => new Flight_DTO
            {
                FlightId = f.FlightId,
                Company = f.Company,
                Source = f.Source,
                Destination = f.Destination,
                FlightDate = f.FlightDate
            }).ToList();
        }

        // פונקציה אסינכרונית לשליפת טיסה לפי ID
        public async Task<Flight_DTO> GetFlightByIdAsync(int id)
        {
            var flight = await flightDal.SelectByIdAsync(id);
            if (flight == null)
                return null;

            return new Flight_DTO
            {
                FlightId = flight.FlightId,
                Company = flight.Company,
                Source = flight.Source,
                Destination = flight.Destination,
                FlightDate = flight.FlightDate
            };
        }

        // פונקציה אסינכרונית להוספת טיסה
        public async Task AddFlightAsync(Flight_DTO flightDto)
        {
            if (flightDto == null)
                return;

            var flight = new FlightTbl
            {
                Company = flightDto.Company,
                Source = flightDto.Source,
                Destination = flightDto.Destination,
                FlightDate = flightDto.FlightDate ?? DateTime.Now // אם FlightDate הוא null, נשתמש בתאריך הנוכחי
            };

            await flightDal.AddFlightAsync(flight);
        }

        // פונקציה אסינכרונית לעדכון טיסה
        public async Task UpdateFlightAsync(Flight_DTO flightDto)
        {
            if (flightDto == null)
                return;

            var flight = new FlightTbl
            {
                FlightId = flightDto.FlightId,
                Company = flightDto.Company,
                Source = flightDto.Source,
                Destination = flightDto.Destination,
                FlightDate = flightDto.FlightDate ?? DateTime.Now // אם FlightDate הוא null, נשתמש בתאריך הנוכחי
            };

            await flightDal.UpdateFlightAsync(flight);
        }

        // פונקציה אסינכרונית למחיקת טיסה
        public async Task<int> DeleteFlightAsync(int flightId)
        {
            return await flightDal.DeleteFlightAsync(flightId);
        }
    }
}