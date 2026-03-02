using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class CartDTOResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
