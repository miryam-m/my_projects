using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class FlightDetails_DAL
    {
        private readonly flyForYouContext db;
        public FlightDetails_DAL(flyForYouContext _db)
        {
            db = _db;
        }

        // פונקציה להוספת פרטי טיסה
     public async Task AddFlightDetailAsync(FlightDetailsTbl flightDetail)
        {
            await db.FlightDetailsTbls.AddAsync(flightDetail);
            await db.SaveChangesAsync();
        }

        // פונקציה לעדכון פרטי טיסה קיימים
        public async Task UpdateFlightDetailAsync(FlightDetailsTbl flightDetail)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            FlightDetailsTbl existingDetail = await db.FlightDetailsTbls.FirstOrDefaultAsync(fd => fd.FlightDetailsId == flightDetail.FlightDetailsId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (existingDetail != null)
            {
                existingDetail.FlightId = flightDetail.FlightId;
                existingDetail.FlightTime = flightDetail.FlightTime;
                existingDetail.NumOfPassengers = flightDetail.NumOfPassengers;
                existingDetail.NumOfAvailableSeats = flightDetail.NumOfAvailableSeats;
                existingDetail.Price = flightDetail.Price;

                await db.SaveChangesAsync();
            }
        }

           // פונקציה למחיקת פרטי טיסה
           public async Task DeleteFlightDetailAsync(int flightDetailId)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            FlightDetailsTbl flightDetail = await db.FlightDetailsTbls.FirstOrDefaultAsync(fd => fd.FlightDetailsId == flightDetailId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (flightDetail != null)
            {
                db.FlightDetailsTbls.Remove(flightDetail);
                await db.SaveChangesAsync();
            }
        }

          // פונקציה לשליפת פרטי טיסה לפי ID
          public async Task<FlightDetailsTbl?> GetFlightDetailAsync(int flightDetailId)
        {
            return await db.FlightDetailsTbls.FirstOrDefaultAsync(fd => fd.FlightDetailsId == flightDetailId);
        }

     // פונקציה לשליפת כל פרטי הטיסה
      public async Task<List<FlightDetailsTbl>> GetAllFlightDetailsAsync()
        {
            return await db.FlightDetailsTbls.ToListAsync();
        }

        // פונקציה לשליפת פרטי טיסה לפי טיסה
        public async Task<List<FlightDetailsTbl>> GetFlightDetailsByFlightIdAsync(int flightId)
        {
            return await db.FlightDetailsTbls.Where(fd => fd.FlightId == flightId).ToListAsync();
        }

    }
}
