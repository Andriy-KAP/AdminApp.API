using AutoMapper;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using CallCenter.DAL.Extensions;
using CallCenter.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<bool> IsSaleExist(string name)
        {
            var sale = await salesRepository.FindBy(_ => _.Name == name).FirstOrDefaultAsync();
            return (sale == null) ? false : true;
        }

        public async Task<PaginatedList<SaleDTO>> GetSales(int pageIndex, int pageSize, int? officeId, string search)
        {
            string searchParam = search == null ? string.Empty : search;
            Expression<Func<Sale, object>> groupIncluding = c => c.Group;
            Expression<Func<Sale, object>> managerIncluding = c => c.User;
            PaginatedList<Sale> sales = null;
            if (officeId != null)
            {
                sales = await salesRepository
                    .AllIncluding(groupIncluding, managerIncluding)
                    .Where(_ => _.GroupId == officeId)
                    .Where(_ => _.Name.Contains(searchParam) || _.Group.Name.Contains(searchParam) || _.User.Email.Contains(searchParam))
                    .ToPaginatedList(pageIndex, pageSize, _ => _.Name);
            }
            else
            {
                sales = await salesRepository
                    .AllIncluding(groupIncluding, managerIncluding)
                    .Where(_ => _.Name.Contains(searchParam) || _.Group.Name.Contains(searchParam) || _.User.Email.Contains(searchParam))
                    .ToPaginatedList(pageIndex, pageSize, _ => _.Name);
            }
            var mappedData = mapper.Map<PaginatedList<Sale>, PaginatedList<SaleDTO>>(sales);
            return mappedData;
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
