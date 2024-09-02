
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using DAL.models;
using DAL;

namespace BL
{
    public class BookingDetails_BL
    {
        private readonly BookingDetails_DAL bookingDetailsDal;

        public BookingDetails_BL(BookingDetails_DAL _bookingDetailsDal)
        {
            bookingDetailsDal = _bookingDetailsDal;
        }

        // פונקציה להוספת פרטי הזמנה
        public async Task AddBookingDetailAsync(BookingDetails_DTO bookingDetailDto)
        {
            var bookingDetail = new BookingDetailsTbl
            {
                BookingId = bookingDetailDto.BookingId,
                NumOfSeats = bookingDetailDto.NumOfSeats,
                SpecialService = bookingDetailDto.SpecialService
            };

            await bookingDetailsDal.AddBookingDetailAsync(bookingDetail);
        }

        // פונקציה לעדכון פרטי הזמנה קיימים
        public async Task UpdateBookingDetailAsync(BookingDetails_DTO bookingDetailDto)
        {
            var bookingDetail = new BookingDetailsTbl
            {
                BookingDetailsId = bookingDetailDto.BookingDetailsId,
                BookingId = bookingDetailDto.BookingId,
                NumOfSeats = bookingDetailDto.NumOfSeats,
                SpecialService = bookingDetailDto.SpecialService
            };

            await bookingDetailsDal.UpdateBookingDetailAsync(bookingDetail);
        }

        // פונקציה למחיקת פרטי הזמנה
        public async Task DeleteBookingDetailAsync(int bookingDetailId)
        {
            await bookingDetailsDal.DeleteBookingDetailAsync(bookingDetailId);
        }

        // פונקציה לשליפת פרטי הזמנה לפי ID
        public async Task<BookingDetails_DTO?> GetBookingDetailAsync(int bookingDetailId)
        {
            var bookingDetail = await bookingDetailsDal.GetBookingDetailAsync(bookingDetailId);
            if (bookingDetail == null) return null;

            return new BookingDetails_DTO
            {
                BookingDetailsId = bookingDetail.BookingDetailsId,
                BookingId = bookingDetail.BookingId,
                NumOfSeats = bookingDetail.NumOfSeats,
                SpecialService = bookingDetail.SpecialService
            };
        }

        // פונקציה לשליפת כל פרטי ההזמנה
        public async Task<List<BookingDetails_DTO>> SelectAllAsync()
        {
            var bookingDetails = await bookingDetailsDal.SelectAllAsync();
            var bookingDetailDtos = new List<BookingDetails_DTO>();

            foreach (var detail in bookingDetails)
            {
                bookingDetailDtos.Add(new BookingDetails_DTO
                {
                    BookingDetailsId = detail.BookingDetailsId,
                    BookingId = detail.BookingId,
                    NumOfSeats = detail.NumOfSeats,
                    SpecialService = detail.SpecialService
                });
            }

            return bookingDetailDtos;
        }

        // פונקציה לשליפת פרטי הזמנה לפי מזהה הזמנה
        public async Task<List<BookingDetails_DTO>> GetBookingDetailsByBookingIdAsync(int bookingId)
        {
            var bookingDetails = await bookingDetailsDal.GetBookingDetailsByBookingIdAsync(bookingId);
            var bookingDetailDtos = new List<BookingDetails_DTO>();

            foreach (var detail in bookingDetails)
            {
                bookingDetailDtos.Add(new BookingDetails_DTO
                {
                    BookingDetailsId = detail.BookingDetailsId,
                    BookingId = detail.BookingId,
                    NumOfSeats = detail.NumOfSeats,
                    SpecialService = detail.SpecialService
                });
            }

            return bookingDetailDtos;
        }
    }
}