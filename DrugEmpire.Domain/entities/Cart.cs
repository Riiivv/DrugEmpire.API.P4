using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.entities
{
    public class Cart
    {
        public int CartId { get; set; }

        public int? UserId { get; set; }   // null hvis guest
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}