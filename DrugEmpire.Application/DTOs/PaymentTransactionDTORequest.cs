using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class PaymentTransactionDTORequest
    {
        public int PaymentTransactionId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // e.g., Completed, Pending, Failed
        public string Provider { get; set; } // e.g., PayPal, Stripe
        public string ProviderReference { get; set; } // e.g., transaction ID from provider
        public DateTime CreatedAt  { get; set; }
    }
}
