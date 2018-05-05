using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Country { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int? SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}
