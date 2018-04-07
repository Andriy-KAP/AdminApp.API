using CallCenter.BLL.DTO;
using CallCenter.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterUser(CallCenter.DAL.Models.UserDomain user);
        Task<IdentityResult> RemoveUser(string email);
        Task<IdentityResult> UpdateUser(IdentityDTO user);
        Task<UserDTO> FindUser(string email);
        Task<IEnumerable<UserDTO>> GetUsers();

        void Dispose();
    }
}
