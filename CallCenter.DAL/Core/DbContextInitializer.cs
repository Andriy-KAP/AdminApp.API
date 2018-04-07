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
            var groups = new List<Group>
            {
                new Group { Name= "Group1"},
                new Group { Name= "Group2"}
            }.ToArray();

            for(int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Email = String.Format("user{0}@email.com", i),
                    HashedPassword = EncryptPassword(String.Format("qwerty{0}", i)),
                    Group = i < 5 ? groups[0] : groups[1],
                    Sales = new List<Sale>
                    {
                        new Sale {
                            Name = String.Format("Sale{0}",i),
                            Group = i<5 ? groups[0] : groups[1],
                            Clients = new List<Client>
                            {
                                new Client {
                                    Name = String.Format("Client{0}", i*2),
                                    Email= String.Format("client{0}@email.com", i)
                                }
                            }
                        }
                    }
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
