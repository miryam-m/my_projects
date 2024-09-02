using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Booking_DAL
    {

        private readonly flyForYouContext db;
        public Booking_DAL(flyForYouContext _db)
        {
            db = _db;
        }

        // מחיקת הזמנה עם מחיקת פרטי הזמנה
        public async Task DeleteBookingAsync(int bookingId)
        {
            // מחיקת פרטי הזמנה הקשורים להזמנה
            List<BookingDetailsTbl> bookingDetails = await db.BookingDetailsTbls.Where(bd => bd.BookingId == bookingId).ToListAsync();

            if (bookingDetails.Count > 0)
            {
                db.BookingDetailsTbls.RemoveRange(bookingDetails);
            }

            // עכשיו, מוחקים את ההזמנה עצמה
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            BookingTbl booking = await db.BookingTbls.FirstOrDefaultAsync(b => b.BookingId == bookingId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (booking != null)
            {
                db.BookingTbls.Remove(booking);
                await db.SaveChangesAsync();
            }
        }

        // פונקציה להוספת הזמנה
        public async Task AddBookingAsync(BookingTbl booking)
        {
            await db.BookingTbls.AddAsync(booking);
            await db.SaveChangesAsync();
        }

        // פונקציה לעדכון הזמנה
        public async Task UpdateBookingAsync(BookingTbl booking)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            BookingTbl existingBooking = await db.BookingTbls.FirstOrDefaultAsync(b => b.BookingId == booking.BookingId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (existingBooking != null)
            {
                existingBooking.PassengerId = booking.PassengerId;
                existingBooking.FlightId = booking.FlightId;
                existingBooking.BookingDate = booking.BookingDate;
                existingBooking.TotalPrice = booking.TotalPrice;

                await db.SaveChangesAsync();
            }
        }

        // פונקציה לשליפת הזמנה לפי ID
        public async Task<BookingTbl?> GetBookingAsync(int bookingId)
        {
            return await db.BookingTbls.Include(b => b.BookingDetailsTbls).FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        // פונקציה לשליפת כל ההזמנות
       public async Task<List<BookingTbl>> SelectAllBookingsAsync()
        {
            return await db.BookingTbls.Include(b => b.BookingDetailsTbls).ToListAsync();
        }
        // פונקציה לשליפת הזמנות לפי נוסע
        public async Task<List<BookingTbl>> GetBookingsByPassengerIdAsync(int passengerId)
        {
            return await db.BookingTbls.Where(b => b.PassengerId == passengerId).Include(b => b.BookingDetailsTbls).ToListAsync();
        }
    }
}