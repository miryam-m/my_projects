using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class BookingDetails_DAL
    {
        private readonly flyForYouContext db;
        public BookingDetails_DAL(flyForYouContext _db)
        {
            db = _db;
        }

       //פונקציה להוספת פרטי הזמנה
        public async Task AddBookingDetailAsync(BookingDetailsTbl bookingDetail)
        {
            await db.BookingDetailsTbls.AddAsync(bookingDetail);
            await db.SaveChangesAsync();
        }

      // פונקציה לעדכון פרטי הזמנה קיימים
       public async Task UpdateBookingDetailAsync(BookingDetailsTbl bookingDetail)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            BookingDetailsTbl existingDetail = await db.BookingDetailsTbls.FirstOrDefaultAsync(bd => bd.BookingDetailsId == bookingDetail.BookingDetailsId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (existingDetail != null)
            {
                existingDetail.BookingId = bookingDetail.BookingId;
                existingDetail.NumOfSeats = bookingDetail.NumOfSeats;
                existingDetail.SpecialService = bookingDetail.SpecialService;

                await db.SaveChangesAsync();
            }
        }

         // פונקציה למחיקת פרטי הזמנה
        public async Task DeleteBookingDetailAsync(int bookingDetailId)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            BookingDetailsTbl bookingDetail = await db.BookingDetailsTbls.FirstOrDefaultAsync(bd => bd.BookingDetailsId == bookingDetailId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (bookingDetail != null)
            {
                db.BookingDetailsTbls.Remove(bookingDetail);
                await db.SaveChangesAsync();
            }
        }

       // פונקציה לשליפת פרטי הזמנה לפי ID
        public async Task<BookingDetailsTbl?> GetBookingDetailAsync(int bookingDetailId)
        {
            return await db.BookingDetailsTbls.FirstOrDefaultAsync(bd => bd.BookingDetailsId == bookingDetailId);
        }

       // פונקציה לשליפת כל פרטי ההזמנה)
       public async Task<List<BookingDetailsTbl>> SelectAllAsync()
        {
            return await db.BookingDetailsTbls.ToListAsync();
        }

      //פונקציה לשליפת פרטי הזמנה לפי הזמנה
      public async Task<List<BookingDetailsTbl>> GetBookingDetailsByBookingIdAsync(int bookingId)
        {
            return await db.BookingDetailsTbls.Where(bd => bd.BookingId == bookingId).ToListAsync();
        }
    }
}
