using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface IGroupService
    {
        Task<PaginatedList<GroupDTO>> GetGroups(int pageIndex, int pageSize);
        Task<IEnumerable<GroupDTO>> GetUserGroups(int groupId);
        Task CreateGroup(GroupDTO group);
        Task UpdateGroup(GroupDTO group);
        Task RemoveGroup(string name);
    }
}
