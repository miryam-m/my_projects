using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class BookingTbl
    {
        public BookingTbl()
        {
            BookingDetailsTbls = new HashSet<BookingDetailsTbl>();
        }

        public int BookingId { get; set; }
        public int? PassengerId { get; set; }
        public int? FlightId { get; set; }
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual FlightTbl? Flight { get; set; }
        public virtual PassengersTbl? Passenger { get; set; }
        public virtual ICollection<BookingDetailsTbl> BookingDetailsTbls { get; set; }
    }
}
