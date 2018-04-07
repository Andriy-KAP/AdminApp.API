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
        public virtual Group Group { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
