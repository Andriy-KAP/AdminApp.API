using AutoMapper;
using CallCenter.API.Filters;
using CallCenter.API.Models;
using CallCenter.API.Response;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CallCenter.API.Controllers
{
    [ModelStateValidationFilter]
    [RoutePrefix("api/Sale")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class SaleController : ControllerBase
    {
        private ISaleService saleService;
        private IMapper mapper;

        public SaleController(ISaleService salesService, IMapper mapper)
        {
            this.saleService = salesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<bool> IsSaleExist(string email)
        {
            return await saleService.IsSaleExist(email);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetSalesCollection([FromUriAttribute] PaginationModel pagination)
        {
            int currentUserOfficeId = GetCurrentUserGroupId();
            PaginatedList<SaleDTO> sales = null;

            sales = (User.IsInRole("Admin")) ?
                await saleService.GetSales(pagination.PageIndex, pagination.PageSize, null, pagination.Search) :
                await saleService.GetSales(pagination.PageIndex, pagination.PageSize, currentUserOfficeId, pagination.Search);

            return Ok(new ResponseSheme(sales, "Ok", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(SaleModel sale)
        {
            SaleDTO targetSale = mapper.Map<SaleModel, SaleDTO>(sale);
            await saleService.CreateSale(targetSale);
            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Update(SaleModel sale)
        {
            SaleDTO targetSale = mapper.Map<SaleModel, SaleDTO>(sale);
            await saleService.UpdateSale(targetSale);
            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Remove(SaleModel sale)
        {
            SaleDTO targetSale = mapper.Map<SaleModel, SaleDTO>(sale);
            await saleService.RemoveSale(targetSale);
            return Ok(new ResponseSheme(null, "Ok", 200));
        }
    }
}
