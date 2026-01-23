using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }

        public string ProductNameSnapshot { get; set; }
        public decimal UnitPriceSnapshot { get; set; }
        public int Quantity { get; set; }
    }

}
