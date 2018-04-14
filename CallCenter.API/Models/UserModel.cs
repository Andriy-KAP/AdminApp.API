using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string HashedPassword { get; set; }
    }
}