using CallCenter.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter.DAL.Core
{
    public class DbContextInitializer : DropCreateDatabaseIfModelChanges<CallCenterContext>
    {
        private const string salt = "2oSFVRKmlkKB2M3z9ErDjw==";

        protected override void Seed(CallCenterContext context)
        {
            var roles = new List<Role>
            {
                new Role { Name="Admin"},
                new Role { Name="Manager"}
            };

            foreach(var role in roles)
            {
                context.Roles.Add(role);
            }

            context.SaveChanges();

            var groups = new List<Group>
            {
                new Group { Name= "Group1"},
                new Group { Name= "Group2"}
            };

            //foreach (var group in groups)
            //{
            //    context.Groups.Add(group);
            //}
            //context.SaveChanges();

            var offices = new List<Office>
            {
                new Office{ Name ="Office 1", Groups = new List<Group>{ new Group { Name= "Office 1 group1" }, new Group { Name = "Office 1 group2" } } },
                new Office{ Name="Office 2", Groups = new List<Group>{ new Group { Name= "Office 2 group1" }, new Group { Name = "Office 2 group2" } }},
                new Office{ Name="Office 3", Groups = new List<Group>{ new Group { Name= "Office 3 group1" }, new Group { Name = "Office 3 group2" } }},
                new Office{ Name="Office 4", Groups = new List<Group>{ new Group { Name= "Office 4 group1" }, new Group { Name = "Office 4 group2" } }}
            }.ToArray();

            foreach (var office in offices)
            {
                context.Offices.Add(office);
            }
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var sale = new Sale
                {
                    Name="sale_user"+1,
                    Group = i % 2 == 0 ? context.Groups.Where(_ => _.Name == "Office 1 group1").FirstOrDefault() : context.Groups.Where(_ => _.Name == "Office 2 group2").FirstOrDefault()
                };

                var user = new User
                {
                    Email = String.Format("user{0}@email.com", i),
                    HashedPassword = EncryptPassword(String.Format("qwerty{0}", i)),
                    Group = i % 2 == 0? context.Groups.Where(_=>_.Name == "Office 1 group1").FirstOrDefault(): context.Groups.Where(_ => _.Name == "Office 2 group2").FirstOrDefault(),
                    //Office = i < 4 ? offices[i] : offices[0],
                    Role = i < 2 ? roles[0] : roles[1],
                    Sales = new List<Sale> { sale }
                };
                context.Users.Add(user);
            }
            context.SaveChanges();
            base.Seed(context);
        }

        private string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }
    }
}
