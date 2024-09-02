using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Booking_DTO
    {
        public int BookingId { get; set; }
        public int? PassengerId { get; set; }
        public int? FlightId { get; set; }
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }
       
    }
}
