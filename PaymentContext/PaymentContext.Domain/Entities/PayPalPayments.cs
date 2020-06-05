using System;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayments : Payment
    {
        public PayPalPayments(
            string transactionCode,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string payer,
            Document document,
            Address address,
            Email email) : base(
            paidDate,
            expireDate,
            total,
            totalPaid,
            payer,
            document,
            address,
            email
        )
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; set; }
    }
}