using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Entities;
using DAL.models;

namespace BL
{
    public class Booking_BL
    {
        private readonly Booking_DAL bookingDal;

        public Booking_BL(Booking_DAL _bookingDal)
        {
            bookingDal = _bookingDal;
        }

        // פונקציה להוספת הזמנה
        public async Task AddBookingAsync(Booking_DTO bookingDto)
        {
            var booking = new BookingTbl
            {
                PassengerId = bookingDto.PassengerId,
                FlightId = bookingDto.FlightId,
                BookingDate = bookingDto.BookingDate,
                TotalPrice = bookingDto.TotalPrice
            };

            await bookingDal.AddBookingAsync(booking);
        }

        // פונקציה לעדכון הזמנה
        public async Task UpdateBookingAsync(Booking_DTO bookingDto)
        {
            var booking = new BookingTbl
            {
                BookingId = bookingDto.BookingId,
                PassengerId = bookingDto.PassengerId,
                FlightId = bookingDto.FlightId,
                BookingDate = bookingDto.BookingDate,
                TotalPrice = bookingDto.TotalPrice
            };

            await bookingDal.UpdateBookingAsync(booking);
        }

        // פונקציה למחיקת הזמנה
        public async Task DeleteBookingAsync(int bookingId)
        {
            await bookingDal.DeleteBookingAsync(bookingId);
        }

        // פונקציה לשליפת הזמנה לפי ID
        public async Task<Booking_DTO?> GetBookingAsync(int bookingId)
        {
            var booking = await bookingDal.GetBookingAsync(bookingId);
            if (booking == null)
            {
                return null;
            }

            return new Booking_DTO
            {
                BookingId = booking.BookingId,
                PassengerId = booking.PassengerId,
                FlightId = booking.FlightId,
                BookingDate = booking.BookingDate,
                TotalPrice = booking.TotalPrice
            };
        }

        // פונקציה לשליפת כל ההזמנות
        public async Task<List<Booking_DTO>> SelectAllBookingsAsync()
        {
            var bookings = await bookingDal.SelectAllBookingsAsync();
            return bookings.Select(b => new Booking_DTO
            {
                BookingId = b.BookingId,
                PassengerId = b.PassengerId,
                FlightId = b.FlightId,
                BookingDate = b.BookingDate,
                TotalPrice = b.TotalPrice
            }).ToList();
        }

        // פונקציה לשליפת הזמנות לפי נוסע
        public async Task<List<Booking_DTO>> GetBookingsByPassengerIdAsync(int passengerId)
        {
            var bookings = await bookingDal.GetBookingsByPassengerIdAsync(passengerId);
            return bookings.Select(b => new Booking_DTO
            {
                BookingId = b.BookingId,
                PassengerId = b.PassengerId,
                FlightId = b.FlightId,
                BookingDate = b.BookingDate,
                TotalPrice = b.TotalPrice
            }).ToList();
        }
    }
}