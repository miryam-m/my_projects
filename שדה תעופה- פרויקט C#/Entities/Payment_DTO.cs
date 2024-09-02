using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Payment_DTO
    {
        public int PaymentId { get; set; }
        public string? CardNumber { get; set; }
       public DateTime? ValidityDate { get; set; }
        public string? Cvv { get; set; }
        public string? OwnerId { get; set; }
        public int? NumOfPayments { get; set; }
    }
}
