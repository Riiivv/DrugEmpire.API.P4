using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public string Status { get; set; }  //pending/shipped/delivered
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}