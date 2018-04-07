using AutoMapper;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.Services
{
    public class SaleService : ISaleService
    {
        private IEntityRepository<Sale> salesRepository;
        private IMapper mapper;

        public SaleService(IEntityRepository<Sale> salesRepository, IMapper mapper)
        {
            this.salesRepository = salesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SaleDTO>> GetSales()
        {
            var sales = await salesRepository.GetAll().ToListAsync();
            var result = mapper.Map<IEnumerable<Sale>, IEnumerable<SaleDTO>>(sales);
            return result;
        }

        public async Task<IEnumerable<SaleDTO>> GetUserSales(int groupId)
        {
            var sales = await salesRepository.FindBy(_ => _.GroupId == groupId).ToListAsync();
            var result = mapper.Map<IEnumerable<Sale>, IEnumerable<SaleDTO>>(sales);
            return result;
        }

        public async Task CreateSale(SaleDTO sale)
        {
            var targetSale = mapper.Map<SaleDTO, Sale>(sale);
            salesRepository.Add(targetSale);
            await salesRepository.Save();
        }

        public async Task UpdateSale(SaleDTO sale)
        {
            var targetSale = mapper.Map<SaleDTO, Sale>(sale);
            salesRepository.Edit(targetSale);
            await salesRepository.Save();
        }

        public async Task RemoveSale(SaleDTO sale)
        {
            var targetSale = Mapper.Map<SaleDTO, Sale>(sale);
            salesRepository.Delete(0);
            await salesRepository.Save();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
