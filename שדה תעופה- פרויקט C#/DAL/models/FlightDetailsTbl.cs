using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class FlightDetailsTbl
    {
        public int FlightDetailsId { get; set; }
        public int? FlightId { get; set; }
        public TimeSpan? FlightTime { get; set; }
        public int? NumOfPassengers { get; set; }
        public int? NumOfAvailableSeats { get; set; }
        public decimal? Price { get; set; }

        public virtual FlightTbl? Flight { get; set; }
    }
}
