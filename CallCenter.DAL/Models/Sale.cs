using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
