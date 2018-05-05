using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public Role Role { get; set; }
        public int? RoleId { get; set; }
    }
}
