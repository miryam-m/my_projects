using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class PaymentTbl
    {
        public int PaymentId { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ValidityDate { get; set; }
        public string? Cvv { get; set; }
        public string? OwnerId { get; set; }
        public int? NumOfPayments { get; set; }
    }
}
