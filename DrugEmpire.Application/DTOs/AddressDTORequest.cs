using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.DTOs
{
    public class AddressDTORequest
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}
