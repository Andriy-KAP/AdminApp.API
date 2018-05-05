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

namespace CallCenter.API.Controllers
{
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
        public async Task<IHttpActionResult> GetSalesCollection()
        {
            var sales = await saleService.GetSales();
            return Ok(new ResponseSheme(sales, "Ok", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateSale(SaleModel sale)
        {
            SaleDTO targetSale = mapper.Map<SaleModel, SaleDTO>(sale);
            await saleService.CreateSale(targetSale);
            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateSale(SaleModel sale)
        {
            SaleDTO targetSale = mapper.Map<SaleModel, SaleDTO>(sale);
            await saleService.UpdateSale(targetSale);
            return Ok(new ResponseSheme(null, "Ok", 200));
        }

        [HttpPost]
        public async Task<IHttpActionResult> RemoveSale(SaleModel sale)
        {
            SaleDTO targetSale = mapper.Map<SaleModel, SaleDTO>(sale);
            await saleService.RemoveSale(targetSale);
            return Ok(new ResponseSheme(null, "Ok", 200));
        }
    }
}
