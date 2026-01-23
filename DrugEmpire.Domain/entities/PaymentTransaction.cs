using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class PaymentTransaction
    {
        public int PaymentTransactionId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }
        public string Status { get; set; }   // Pending/Succeeded/Failed
        public string Provider { get; set; } // Stripe, etc.
        public string ProviderReference { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
