using AutoMapper;
using CallCenter.API.Models;
using CallCenter.API.Response;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CallCenter.API.Controllers
{
    [Authorize]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class InfoController : ControllerBase
    {
        private IMapper mapper;
        private IInfoService service;

        public InfoController(IMapper mapper, IInfoService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetInfo()
        {
            InfoDTO infoDto = await service.GetInfo();
            InfoModel info = mapper.Map<InfoDTO, InfoModel>(infoDto);

            if (info != null)
            {
                return Ok(new ResponseSheme(info, "EverythingOk", 200));
            }
            return BadRequest();
        }
    }
}
