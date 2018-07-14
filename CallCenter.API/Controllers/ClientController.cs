using AutoMapper;
using CallCenter.API.Filters;
using CallCenter.API.Models;
using CallCenter.API.Response;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using System.Threading.Tasks;
using System.Web.Http;

namespace CallCenter.API.Controllers
{
    [Authorize]
    [ModelStateValidationFilter]
    public class ClientController : ControllerBase
    {
        private IClientService clientService;
        private IMapper mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            this.clientService = clientService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<bool> IsClientExist(string username)
        {
            return await clientService.IsClientExist(username);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Assign([FromBody]ClientAssignModel model)
        {
            await clientService.Assign(model.ClientId, model.SaleId);
            return Ok(new ResponseSheme(null, "EverythingOk", 200));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetClientsCollection([FromUri]PaginationModel pagination)
        {
            int currentGroupId = GetCurrentUserGroupId();
            PaginatedList<ClientDTO> clients = null;
            clients = (User.IsInRole("Admin")) ?
                await clientService.GetClients(pagination.PageIndex, pagination.PageSize, null, pagination.Search) :
                await clientService.GetClients(pagination.PageIndex, pagination.PageSize, currentGroupId, pagination.Search);

            var mappedClients = mapper.Map<PaginatedList<ClientDTO>, PaginatedList<ClientModel>>(clients);

            return (clients != null) ?
                (IHttpActionResult)Ok(new ResponseSheme(mappedClients, "EverythingOk", 200)) :
                InternalServerError();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] ClientModel client)
        {
            var clientDTO = mapper.Map<ClientModel, ClientDTO>(client);
            var createdClient =  await clientService.Create(clientDTO);

            return (createdClient != null) ?
                (IHttpActionResult)Ok(new ResponseSheme(createdClient, "EverythingOk", 200)) :
                InternalServerError();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Edit([FromBody]ClientModel client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var clientDTO = mapper.Map<ClientModel, ClientDTO>(client);
            await clientService.Edit(clientDTO);

            return Ok(new ResponseSheme(null, "EverythingOk", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Remove([FromBody]ClientModel client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var mappedClient = mapper.Map<ClientModel, ClientDTO>(client);
            await clientService.Delete(mappedClient);

            return Ok(new ResponseSheme(null, "EverythingOk", 200));
        }
    }
}
