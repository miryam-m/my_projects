using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class PassengersTbl
    {
        public PassengersTbl()
        {
            BookingTbls = new HashSet<BookingTbl>();
        }

        public int? PassengersId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Id { get; set; }
        public string? City { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<BookingTbl> BookingTbls { get; set; }
    }
}
