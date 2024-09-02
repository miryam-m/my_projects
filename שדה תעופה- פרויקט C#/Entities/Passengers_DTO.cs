using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Passengers_DTO
    {
        public int? PassengersId { get; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Id { get; set; }
        public string? City { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; } 
    }
}
