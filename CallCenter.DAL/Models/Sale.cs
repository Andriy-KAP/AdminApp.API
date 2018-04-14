using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
