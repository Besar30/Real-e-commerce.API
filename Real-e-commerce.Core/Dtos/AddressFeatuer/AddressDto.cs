using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Dtos.AddressFeatuer
{
    public class AddressDto
    {
        [Required]
        public  string Line1 { get; set; }
        public  string? Line2 { get; set; }
        [Required]
        public  string City { get; set; }
        [Required]
        public  string State { get; set; }
        [Required]
        public  string PostalCode { get; set; }
        [Required]
        public  string Country { get; set; }
    }
}
