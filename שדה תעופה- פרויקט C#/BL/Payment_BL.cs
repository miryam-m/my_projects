
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.models;
using DAL;
using Entities;

namespace BL
{
    public class Payment_BL
    {
        private readonly Payment_DAL PaymentDAL;

        public Payment_BL(Payment_DAL payDAL)
        {
            PaymentDAL = payDAL;
        }

        public Payment_DTO? ConvertToDTO(PaymentTbl pay)
        {
            if (pay == null) return null;

            return new Payment_DTO
            {
                CardNumber = pay.CardNumber,
                //ValidityDate = pay.ValidityDate,
                Cvv = pay.Cvv,
                OwnerId = pay.OwnerId,
                NumOfPayments = pay.NumOfPayments
            };
        }

        public List<Payment_DTO> ConvertToDTO(List<PaymentTbl> payments)
        {
            List<Payment_DTO> paymentDTOs = new List<Payment_DTO>();
            foreach (var payment in payments)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                paymentDTOs.Add(ConvertToDTO(payment));
#pragma warning restore CS8604 // Possible null reference argument.
            }
            return paymentDTOs;
        }

        public PaymentTbl ConvertFromDTO(Payment_DTO dto)
        {
            return new PaymentTbl
            {
                CardNumber = dto.CardNumber,
                //ValidityDate = dto.ValidityDate,
                Cvv = dto.Cvv,
                OwnerId = dto.OwnerId,
                NumOfPayments = dto.NumOfPayments
            };
        }

        public async Task AddPaymentAsync(Payment_DTO paymentDTO)
        {
            var payment = ConvertFromDTO(paymentDTO);
            ValidatePayment(payment);
            await PaymentDAL.AddPaymentAsync(payment);
        }

        public async Task UpdatePaymentAsync(Payment_DTO paymentDTO)
        {
            var payment = ConvertFromDTO(paymentDTO);
            ValidatePayment(payment);
            await PaymentDAL.UpdatePaymentAsync(payment);
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            await PaymentDAL.DeletePaymentAsync(paymentId);
        }

        public async Task<Payment_DTO?> GetPaymentAsync(int paymentId)
        {
            var payment = await PaymentDAL.GetPaymentAsync(paymentId);
#pragma warning disable CS8604 // Possible null reference argument.
            return ConvertToDTO(payment);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public async Task<List<Payment_DTO>> GetAllPaymentsAsync()
        {
            var payments = await PaymentDAL.GetAllPaymentsAsync();
            return ConvertToDTO(payments);
        }

        public async Task<List<Payment_DTO>> GetPaymentsByOwnerIdAsync(string ownerId)
        {
            var payments = await PaymentDAL.GetPaymentsByOwnerIdAsync(ownerId);
            return ConvertToDTO(payments);
        }

        private void ValidatePayment(PaymentTbl payment)
        {
            if (string.IsNullOrEmpty(payment.CardNumber))
                throw new ArgumentException("Card number is required.");
            //if (payment.ValidityDate == null || payment.ValidityDate < DateTime.Now)
            //    throw new ArgumentException("Invalid validity date.");
            if (string.IsNullOrEmpty(payment.Cvv) || payment.Cvv.Length != 3)
                throw new ArgumentException("CVV must be 3 digits.");
        }
    }
}
