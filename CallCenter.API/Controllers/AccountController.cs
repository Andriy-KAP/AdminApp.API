using AutoMapper;
using CallCenter.API.Filters;
using CallCenter.API.Models;
using CallCenter.API.Response;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CallCenter.API.Controllers
{
    [ModelStateValidationFilter]
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
