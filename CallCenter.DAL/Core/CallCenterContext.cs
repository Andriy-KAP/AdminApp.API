using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CallCenter.DAL.Models;

namespace CallCenter.DAL.Core
{
    public class CallCenterContext:DbContext//: IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<User> Users { get; set; }

        static CallCenterContext()
        {
            Database.SetInitializer<CallCenterContext>(new DbContextInitializer());
        }

        public CallCenterContext():base("CallCenterContext")
        {
            
        }

        public static CallCenterContext GetContext()
        {
            return new CallCenterContext();
        }

        public static void InitDb()
        {
            GetContext().Database.Initialize(true);
        }
    }
}
