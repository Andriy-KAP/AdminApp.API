using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Models
{
    public class Deal
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public int? SaleId { get; set; }
        public Sale Sale { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }
    }
}
