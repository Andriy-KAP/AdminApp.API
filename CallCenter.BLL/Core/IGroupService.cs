using CallCenter.BLL.DTO;
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
        Task<IEnumerable<GroupDTO>> GetGroups();
        Task<IEnumerable<GroupDTO>> GetUserGroups(int groupId);
        Task CreateGroup(GroupDTO group);
        Task UpdateGroup(GroupDTO group);
        Task RemoveGroup(string name);
    }
}
