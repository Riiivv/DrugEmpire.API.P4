using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class InventoryItem
    {
       public int InventoryItemId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReservedQuantity { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
