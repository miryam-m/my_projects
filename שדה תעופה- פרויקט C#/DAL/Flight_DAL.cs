using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Flight_DAL
    {
        private readonly flyForYouContext db;

        public Flight_DAL(flyForYouContext _db)
        {
            db = _db;
        }

        // פונקציה סינכרונית לשליפת כל הטיסות
        public List<FlightTbl> SelectAll()
        {
            return db.FlightTbls.Include(f => f.BookingTbls).Include(f => f.FlightDetailsTbls).ToList();
        }

        // פונקציה אסינכרונית לשליפת כל הטיסות
        public async Task<List<FlightTbl>> SelectAllAsync()
        {
            return await db.FlightTbls.Include(f => f.BookingTbls).Include(f => f.FlightDetailsTbls).ToListAsync();
        }

        // פונקציה אסינכרונית להוספת טיסה
        public async Task AddFlightAsync(FlightTbl flight)
        {
            if (flight == null)
                return;
            db.FlightTbls.Add(flight);
            await db.SaveChangesAsync();
        }

        // פונקציה סינכרונית לשליפת טיסה לפי ID
        public FlightTbl SelectById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return db.FlightTbls.Include(f => f.BookingTbls).Include(f => f.FlightDetailsTbls).FirstOrDefault(f => f.FlightId == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        // פונקציה אסינכרונית לשליפת טיסה לפי ID
        public async Task<FlightTbl> SelectByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await db.FlightTbls.Include(f => f.BookingTbls).Include(f => f.FlightDetailsTbls).FirstOrDefaultAsync(f => f.FlightId == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        // פונקציה אסינכרונית למחיקת טיסה 
        public async Task<int> DeleteFlightAsync(int flightId)
        {
            int res = -1;

            // מחיקת פרטי הטיסה
            var flightDetails = await db.FlightDetailsTbls.Where(fd => fd.FlightId == flightId).ToListAsync();
            if (flightDetails.Any())
            {
                db.FlightDetailsTbls.RemoveRange(flightDetails);
            }

            // מחיקת הזמנות הקשורות
            var bookings = await db.BookingTbls.Where(b => b.FlightId == flightId).ToListAsync();
            if (bookings.Any())
            {
                db.BookingTbls.RemoveRange(bookings);
            }

            // מחיקת הטיסה עצמה
            var flight = await SelectByIdAsync(flightId);
            if (flight != null)
            {
                db.FlightTbls.Remove(flight);
                res = await db.SaveChangesAsync();
            }
            return res;
        }

        // פונקציה אסינכרונית לעדכון טיסה
        public async Task<int> UpdateFlightAsync(FlightTbl flight)
        {
            int res = -1;

            var existingFlight = await SelectByIdAsync(flight.FlightId);
            if (existingFlight != null)
            {
                existingFlight.Company = flight.Company;
                existingFlight.Source = flight.Source;
                existingFlight.Destination = flight.Destination;
                existingFlight.FlightDate = flight.FlightDate;

                res = await db.SaveChangesAsync();
            }
            return res;
        }
    }
}