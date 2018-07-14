using AutoMapper;
using CallCenter.API.Filters;
using CallCenter.API.Models;
using CallCenter.API.Response;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CallCenter.API.Controllers
{
    [ModelStateValidationFilter]
    [Authorize]
    [RoutePrefix("api/Group")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class GroupController: ControllerBase
    {
        private IGroupService groupService;
        private IMapper mapper;

        public GroupController(IGroupService groupService, AutoMapper.IMapper mapper)
        {
            this.groupService = groupService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGroupsCollection([FromUri]PaginationModel pagination)
        {
            int currentUserGroupId = GetCurrentUserGroupId();
            PaginatedList<GroupDTO> groups = null;
            groups = (User.IsInRole("Admin")) ?
                await groupService.GetGroups(pagination.PageIndex, pagination.PageSize, null, pagination.Search) :
                await groupService.GetGroups(pagination.PageIndex, pagination.PageSize, currentUserGroupId, pagination.Search);
            PaginatedList<GroupModel> mappedGroups = mapper.Map<PaginatedList<GroupDTO>, PaginatedList<GroupModel>>(groups);
            return Ok(new ResponseSheme(mappedGroups, "EverythingOk", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateNewGroup([FromBody]GroupModel group)
        {
            GroupDTO groupDto = mapper.Map<GroupModel, GroupDTO>(group);
            await groupService.CreateGroup(groupDto);

            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpPost]
        public IHttpActionResult UpdateGroup([FromBody]GroupModel group)
        {
            GroupDTO groupDTO = mapper.Map<GroupModel, GroupDTO>(group);
            groupService.UpdateGroup(groupDTO);

            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpPost]
        public IHttpActionResult RemoveGroup([FromBody]GroupModel group)
        {
            groupService.RemoveGroup(group.Id);

            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGroupSales([FromUri]GroupModel group)
        {
            var mappedGroup = mapper.Map<GroupModel, GroupDTO>(group);
            var groupSales = await groupService.GetGroupSales(mappedGroup);

            return Ok(new ResponseSheme(groupSales, "EverythingIsOk", 200));
        }
    }
}