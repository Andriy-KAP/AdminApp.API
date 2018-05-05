using AutoMapper;
using CallCenter.API.Models;
using CallCenter.API.Response;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CallCenter.API.Controllers
{
    [RoutePrefix("api/Account")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AccountController : ControllerBase
    {
        private IAuthService authService;
        private AutoMapper.IMapper mapper;

        public AccountController(IAuthService authService, AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody]UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDTO userDTO = mapper.Map<UserModel, UserDTO>(user);
            string token = await authService.Login(userDTO);
            if(token != null)
            {
                return Ok(new ResponseSheme(token, "EverythingOk", 200));
            }
            return NotFound();
        }
        
    }
}
