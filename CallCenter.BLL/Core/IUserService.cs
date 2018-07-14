using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface IUserService
    {
        Task<PaginatedList<UserDTO>> GetUsers(int pageIndex, int pageSize, int? groupId, string search);
        Task<UserDTO> Create(UserDTO user);
        Task Edit(UserDTO user);
        Task Delete(int id);
        Task<bool> IsUserExist(string username);
        Task<int> GetUsersCount();
        Task<int> GetAdminsCount();
        Task<int> GetManagersCount();
    }
}
