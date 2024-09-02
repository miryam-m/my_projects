using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class FlightTbl
    {
        public FlightTbl()
        {
            BookingTbls = new HashSet<BookingTbl>();
            FlightDetailsTbls = new HashSet<FlightDetailsTbl>();
        }

        public int FlightId { get; set; }
        public string Company { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; } 
        public DateTime FlightDate { get; set; }

        public virtual ICollection<BookingTbl> BookingTbls { get; set; }
        public virtual ICollection<FlightDetailsTbl> FlightDetailsTbls { get; set; }
    }
}
