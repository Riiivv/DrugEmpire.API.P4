using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class OrderDTORequest
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

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
    }
}
