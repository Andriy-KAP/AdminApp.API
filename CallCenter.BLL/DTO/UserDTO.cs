using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Password { get; set; }
    }
}
