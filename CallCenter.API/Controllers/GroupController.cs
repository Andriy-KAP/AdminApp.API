using AutoMapper;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CallCenter.API.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Group")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class GroupController: ApiController
    {
        private IGroupService groupService;
        private IMapper mapper;

        public GroupController(IGroupService groupService, AutoMapper.IMapper mapper)
        {
            this.groupService = groupService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGroupsCollection()
        {
            var groups = await groupService.GetGroups();
            List<GroupModel> groupsDTO = mapper.Map<List<GroupDTO>, List<GroupModel>>(groups);
            return Ok(new ResponseSheme(groups, "Ok", 200, groups.Count()));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateNewGroup([FromBody]GroupModel group)
        {
            GroupDTO groupDto = mapper.Map<GroupModel, GroupDTO>(group);
            await groupService.CreateGroup(groupDto);

            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpPut]
        public IHttpActionResult UpdateGroup([FromBody]GroupModel group)
        {
            GroupDTO groupDTO = mapper.Map<GroupModel, GroupDTO>(group);
            groupService.UpdateGroup(groupDTO);

            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpDelete]
        public IHttpActionResult RemoveGroup([FromUri]string name)
        {
            groupService.RemoveGroup(name);

            return Ok(new ResponseSheme(null, "Ok", 200));
        }
    }
}