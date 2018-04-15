using CallCenter.BLL.Core;
using System.Linq;
using CallCenter.DAL.Models;
using CallCenter.DAL.Core;
using CallCenter.BLL.DTO;
using AutoMapper;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;
using CallCenter.DAL.Extensions;

namespace CallCenter.BLL.Services
{
    public class GroupService : IGroupService
    {
        private IEntityRepository<Group> groupRepository;
        private IMapper mapper;

        public GroupService(IEntityRepository<Group> groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<GroupDTO>> GetGroups(int pageIndex, int pageSize)
        {
            var groups = await groupRepository.GetAll().ToPaginatedList(pageIndex, pageIndex, _=>_.Name);
            var result = mapper.Map<PaginatedList<Group>, PaginatedList<GroupDTO>>(groups);
            return result;
        }

        public async Task<IEnumerable<GroupDTO>> GetUserGroups(int groupId)
        {
            var groups = await groupRepository.FindBy(_ => _.Id == groupId).ToListAsync();
            var result = mapper.Map<IEnumerable<Group>, IEnumerable<GroupDTO>>(groups);
            return result;
        }

        public async Task CreateGroup(GroupDTO group)
        {
            Group newGroup = mapper.Map<GroupDTO, Group>(group);
            groupRepository.Add(newGroup);
            await groupRepository.Save();
        }

        public async Task RemoveGroup(string name)
        {
            var targetGroup = await groupRepository.FindBy(_ => _.Name == name).ToListAsync();
            groupRepository.Delete(0);
            await groupRepository.Save();
        }

        public async Task UpdateGroup(GroupDTO group)
        {
            Group targetGroup = mapper.Map<GroupDTO, Group>(group);
            groupRepository.Edit(targetGroup);
            await groupRepository.Save();
        }
    }
}