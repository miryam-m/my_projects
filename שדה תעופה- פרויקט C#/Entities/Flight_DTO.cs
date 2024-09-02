using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Flight_DTO
    {
        public int FlightId { get; set; }
        public string? Company { get; set; }
        public string? Source { get; set; } 
        public string? Destination { get; set; } 
        public DateTime? FlightDate { get; set; }
    }
}
