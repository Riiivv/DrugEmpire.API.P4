using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Category> ProductCategories { get; set; } = new List<Category>();
    }
}
