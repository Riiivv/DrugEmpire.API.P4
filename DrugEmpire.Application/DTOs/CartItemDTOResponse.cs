using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class CartItemDTOResponse
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
