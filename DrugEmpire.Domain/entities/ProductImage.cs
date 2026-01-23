using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Url { get; set; }
        public int SortOrder { get; set; }
    }
}