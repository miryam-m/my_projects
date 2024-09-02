using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Passengers_DAL
    {

        private readonly flyForYouContext db;
        public Passengers_DAL(flyForYouContext _db)
        {
            db = _db;
        }

        //פונקציה סינכרונית לשליפת כל הנוסעים
        public List<PassengersTbl> SelectAll()
        {
            return db.PassengersTbls.ToList();
        }

        //פונקציה אסינכרונית לשליפת כל הנוסעים
        public async Task<List<PassengersTbl>> SelectAllAsync()
        {
            return await db.PassengersTbls.ToListAsync();
        }

        public async Task<PassengersTbl> AddPassenger(PassengersTbl p)
        {
            await db.PassengersTbls.AddAsync(p);
            await db.SaveChangesAsync();
            return p; // מחזיר את הנוסע שנוסף
        }


        //פונקציה סינכרונית להחזרת לקוח על פי מספר מזהה
        public PassengersTbl SelectById(int id)
        {
            var passenger = db.PassengersTbls.FirstOrDefault(p => p.PassengersId == id);

            if (passenger == null)
            {
                throw new KeyNotFoundException($"Passenger with ID {id} was not found.");
            }
            return passenger;
        }

        //פונקציה אסינכרונית להחזרת לקוח על פי מספר מזהה

        public async Task<PassengersTbl> SelectPassengerByIdAsync(int id)
        {
            var passenger = await db.PassengersTbls.FirstOrDefaultAsync(p => p.PassengersId == id);

            if (passenger == null)
            {
                throw new KeyNotFoundException($"Passenger with ID {id} was not found.");
            }

            return passenger;
        }

        //מחיקת נוסע 
        public async Task DeletePassengerAsync(int passengerId)
        {

            // שליפת הנוסע לפי ה-ID
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            PassengersTbl passenger = await db.PassengersTbls.FirstOrDefaultAsync(p => p.PassengersId == passengerId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (passenger != null)
            {
                // שליפת כל ההזמנות של הנוסע
                List<BookingTbl> bookings = await db.BookingTbls
                    .Where(b => b.PassengerId == passengerId) // נניח ש-BookingTbl מכילה עמודה בשם PassengerId
                    .ToListAsync();

                // מחיקת פרטי ההזמנה של כל הזמנה
                foreach (BookingTbl booking in bookings)
                {
                    // שליפת פרטי ההזמנה על פי BookingId
                    List<BookingDetailsTbl> bookingDetails = await db.BookingDetailsTbls
                        .Where(bd => bd.BookingId == booking.BookingId) // נניח ש-BookingDetailsTbl מכילה עמודה בשם BookingId
                        .ToListAsync();
                    // מחיקת פרטי ההזמנה
                    foreach (BookingDetailsTbl detail in bookingDetails)
                    {
                        db.BookingDetailsTbls.Remove(detail);
                    }
                    // מחיקת ההזמנה
                    db.BookingTbls.Remove(booking);
                }
                // מחיקת הנוסע
                db.PassengersTbls.Remove(passenger);
                await db.SaveChangesAsync(); // שמירת השינויים במסד הנתונים
            }
        }

        //עדכון נוסע
        public async Task UpdatePassengerAsync(PassengersTbl passenger)
        {
            // שליפת הנוסע הקיים לפי ה-ID
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            PassengersTbl existingPassenger = await db.PassengersTbls.FirstOrDefaultAsync(p => p.PassengersId == passenger.PassengersId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (existingPassenger != null)
            {
                // עדכון השדות
                existingPassenger.FirstName = passenger.FirstName;
                existingPassenger.LastName = passenger.LastName;
                existingPassenger.City = passenger.City;
                existingPassenger.Adress = passenger.Adress;
                existingPassenger.Phone = passenger.Phone;
                existingPassenger.Email = passenger.Email;

                // שמירת השינויים
                await db.SaveChangesAsync();
            }
        }
    }
}

