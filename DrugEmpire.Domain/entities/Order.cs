using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string OrderNumber { get; set; }
        public string Status { get; set; } // Pending/Paid/Shipped/Cancelled

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }


        // Shipping snapshot (MVP)
        public string ShippingName { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingPhoneNumber { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<PaymentTransaction> Payments { get; set; } = new List<PaymentTransaction>();

        public Shipment Shipment { get; set; } // 0..1
    }

}
