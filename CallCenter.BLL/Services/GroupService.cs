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
using System;
using System.Linq.Expressions;

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

        public async Task<PaginatedList<GroupDTO>> GetGroups(int pageIndex, int pageSize, int? groupId, string search)
        {
            string searchParam = search == null ? string.Empty : search;
            Expression<Func<Group, object>> officeIncluding = o => o.Office;
            PaginatedList<Group> groups = null;
            var currentGroup = await groupRepository.FindBy(_ => _.Id == groupId).FirstOrDefaultAsync();
            if(groupId != null)
            {
                groups = await groupRepository
                    .FindBy(_ => _.OfficeId == currentGroup.OfficeId)
                    .Where(_=>_.Name.Contains(searchParam) || _.Id.ToString().Contains(searchParam))
                    .Include(_=>_.Office).ToPaginatedList(pageIndex, pageSize, _=>_.Name);
            }
            else
            {
                groups = await groupRepository
                    .AllIncluding(_=>_.Office)
                    .Where(_=>_.Name.Contains(searchParam) || _.Id.ToString().Contains(searchParam))
                    .ToPaginatedList(pageIndex, pageSize, _ => _.Name);
            }
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

        public async Task RemoveGroup(int id)
        {
            groupRepository.Delete(id);
            await groupRepository.Save();
        }

        public async Task UpdateGroup(GroupDTO group)
        {
            Group targetGroup = mapper.Map<GroupDTO, Group>(group);
            groupRepository.Edit(targetGroup);
            await groupRepository.Save();
        }

        public async Task<GroupDTO> GetGroupSales(GroupDTO group)
        {
            var groupModel =  await groupRepository.FindBy(_ => _.Id == group.Id).Include(_ => _.Sales).FirstOrDefaultAsync();
            GroupDTO groupDto = mapper.Map<Group, GroupDTO>(groupModel);

            return groupDto;
        }
    }
}