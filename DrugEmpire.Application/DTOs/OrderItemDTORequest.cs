using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class OrderItemDTORequest
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public string ProductNameSnapshot { get; set; }
        public decimal UnitPriceSnapshot { get; set; }
        public int Quantity { get; set; }
    }
}
