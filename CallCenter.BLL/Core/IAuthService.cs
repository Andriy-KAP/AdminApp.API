using CallCenter.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface IAuthService
    {
        Task<string> Login(UserDTO userDTO);
        bool IsUserExist(string userEmail);
    }
}
