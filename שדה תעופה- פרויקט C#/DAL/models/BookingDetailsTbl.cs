using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class BookingDetailsTbl
    {
        public int BookingDetailsId { get; set; }
        public int? BookingId { get; set; }
        public int? NumOfSeats { get; set; }
        public bool? SpecialService { get; set; }

        public virtual BookingTbl? Booking { get; set; }
    }
}
