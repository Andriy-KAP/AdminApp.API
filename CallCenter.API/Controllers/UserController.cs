using AutoMapper;
using CallCenter.API.Models;
using CallCenter.API.Response;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using CallCenter.DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace CallCenter.API.Controllers
{
    [Authorize]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private ICryptoService cryptoService;
        private IMapper mapper;

        public UserController(IUserService userService, ICryptoService cryptoService, IMapper mapper)
        {
            this.userService = userService;
            this.cryptoService = cryptoService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<bool> IsUserExist(string username)
        {
            return await userService.IsUserExist(username);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGlobalUserInfo()
        {
            var globalUserInfo = new
            {
                AllUsersCount = await userService.GetUsersCount(),
                AdminsCount = await userService.GetAdminsCount(),
                ManagersCount = await userService.GetManagersCount()
            };

            return Ok(new ResponseSheme(globalUserInfo, "EverythingOk", 200));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserCollection([FromUriAttribute] PaginationModel pagination)
       {
            int currentUserOfficeId = GetCurrentUserGroupId();
            PaginatedList<UserDTO> users = null;

            users = (User.IsInRole("Admin")) ?
                await userService.GetUsers(pagination.PageIndex, pagination.PageSize, null, pagination.Search) :
                await userService.GetUsers(pagination.PageIndex, pagination.PageSize, currentUserOfficeId, pagination.Search);

            var mappedUsers = mapper.Map<PaginatedList<UserDTO>, PaginatedList<UserModel>>(users);

            return (users != null) ? 
                (IHttpActionResult) Ok(new ResponseSheme(mappedUsers, "EverythingOk", 200)) : 
                InternalServerError();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Edit(UserModel user)
        {
            var userDto = mapper.Map<UserModel, UserDTO>(user);
            await userService.Edit(userDto);

            return Ok(new ResponseSheme(null, "EverythingOk", 200));
        }
        
        [HttpPost]
        public async Task<IHttpActionResult> Remove(UserModel user)
        {
            await userService.Delete(user.Id);
            return Ok(new ResponseSheme(null, "EverythingOk", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(UserModel model)
        {
            var targetUser = mapper.Map<UserModel, UserDTO>(model);
            targetUser.HashedPassword = cryptoService.EncryptPassword(model.Password);
            var newUser = await userService.Create(targetUser);
            
            if(newUser != null)
            {
                return Ok(new ResponseSheme(newUser, "EverythingOk", 200));
            }
            return InternalServerError();
        }
    }
}
