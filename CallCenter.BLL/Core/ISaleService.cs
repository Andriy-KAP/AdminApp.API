using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface ISaleService
    {
        Task<PaginatedList<SaleDTO>> GetSales(int pageIndex, int pageSize, int? officeId, string search);
        Task<IEnumerable<SaleDTO>> GetUserSales(int groupId);
        Task CreateSale(SaleDTO sale);
        Task UpdateSale(SaleDTO sale);
        Task RemoveSale(SaleDTO sale);
        Task<bool> IsSaleExist(string name);
        Task SaveChanges();
    }
}
