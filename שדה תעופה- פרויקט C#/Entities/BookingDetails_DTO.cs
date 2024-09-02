using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookingDetails_DTO
    {
        public int BookingDetailsId { get; set; }
        public int? BookingId { get; set; }
        public int? NumOfSeats { get; set; }
        public bool? SpecialService { get; set; }
    }
}
