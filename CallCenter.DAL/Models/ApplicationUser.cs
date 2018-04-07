using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
