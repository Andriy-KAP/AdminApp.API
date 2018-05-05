using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Models
{
    public class Office
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
