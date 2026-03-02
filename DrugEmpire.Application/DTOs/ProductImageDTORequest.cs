using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class ProductImageDTORequest
    {
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Url { get; set; }
        public int SortOrder { get; set; }
    }
}
