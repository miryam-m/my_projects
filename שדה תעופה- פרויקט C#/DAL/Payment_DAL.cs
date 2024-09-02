using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Payment_DAL
    {
        private readonly flyForYouContext db;
        public Payment_DAL(flyForYouContext _db)
        {
            db = _db;
        }

       //  פונקציה להוספת תשלום חדש
          public async Task AddPaymentAsync(PaymentTbl payment)
        {
            await db.PaymentTbls.AddAsync(payment);
            await db.SaveChangesAsync();
        }

       // פונקציה לעדכון פרטי תשלום קיים
        public async Task UpdatePaymentAsync(PaymentTbl payment)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            PaymentTbl existingPayment = await db.PaymentTbls.FirstOrDefaultAsync(p => p.PaymentId == payment.PaymentId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (existingPayment != null)
            {
                existingPayment.CardNumber = payment.CardNumber;
                existingPayment.ValidityDate = payment.ValidityDate;
                existingPayment.Cvv = payment.Cvv;
                existingPayment.OwnerId = payment.OwnerId;
                existingPayment.NumOfPayments = payment.NumOfPayments;

                await db.SaveChangesAsync();
            }
        }
           // פונקציה למחיקת תשלום

           public async Task DeletePaymentAsync(int paymentId)
             {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            PaymentTbl payment = await db.PaymentTbls.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (payment != null)
            {
                db.PaymentTbls.Remove(payment);
                await db.SaveChangesAsync();
            }
        }
        //פונקציה לשליפת פרטי תשלום לפי ID
          public async Task<PaymentTbl?> GetPaymentAsync(int paymentId)
        {
            return await db.PaymentTbls.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        //פונקציה לשליפת כל התשלומים
        public async Task<List<PaymentTbl>> GetAllPaymentsAsync()
        {
            return await db.PaymentTbls.ToListAsync();
        }

        //פונקציה לשליפת תשלומים לפי בעל תשלום
          public async Task<List<PaymentTbl>> GetPaymentsByOwnerIdAsync(string ownerId)
        {
            return await db.PaymentTbls.Where(p => p.OwnerId == ownerId).ToListAsync();
        }

    }
}
