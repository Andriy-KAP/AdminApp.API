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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace CallCenter.API.Controllers
{
    [Authorize]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ApiController
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
        public async Task<IHttpActionResult> GetUserCollection([FromUriAttribute] PaginationModel pagination)
        {
            var users =  await userService.GetUsers(pagination.PageIndex, pagination.PageSize);
            var mappedUsers = mapper.Map<PaginatedList<UserDTO>, PaginatedList<UserModel>>(users);
            if(users != null)
            {
                return Ok(new ResponseSheme(mappedUsers, "EverythingOk", 200));
            }
            return InternalServerError();
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
