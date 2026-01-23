using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public string Sku { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<Category> ProductCategories { get; set; } = new List<Category>();

        public InventoryItem InventoryItem { get; set; }
    }
}
