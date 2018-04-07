using CallCenter.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDTO>> GetSales();
        Task<IEnumerable<SaleDTO>> GetUserSales(int groupId);
        Task CreateSale(SaleDTO sale);
        Task UpdateSale(SaleDTO sale);
        Task RemoveSale(SaleDTO sale);
        Task SaveChanges();
    }
}
