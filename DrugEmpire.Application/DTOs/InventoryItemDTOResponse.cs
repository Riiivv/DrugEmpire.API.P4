using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class InventoryItemDTOResponse
    {
        public int InventoryItemId { get; set; }
        public int? ProductId { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReservedQuantity { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
