using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FlightDetails_DTO
    {
        public int FlightDetailsId { get; set; }
        public int? FlightId { get; set; }
        public TimeSpan? FlightTime { get; set; }
        public int? NumOfPassengers { get; set; }
        public int? NumOfAvailableSeats { get; set; }
        public decimal? Price { get; set; }
    }
}
