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
        private IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserCollection([FromUriAttribute] PaginationModel pagination)
        {
            var t = HttpContext.Current;

            var users =  await userService.GetUsers(pagination.PageIndex, pagination.PageSize);
            var mappedUsers = mapper.Map<PaginatedList<UserDTO>, PaginatedList<UserModel>>(users);
            if(users != null)
            {
                return Ok(new ResponseSheme(mappedUsers, "EverythingOk", 200));
            }
            return InternalServerError();
        }

        [HttpGet]
        public HttpResponseMessage Created()
        {
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
