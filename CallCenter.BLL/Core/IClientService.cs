using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface IClientService
    {
        Task<PaginatedList<ClientDTO>> GetClients(int pageIndex, int pageSize, int? groupId, string search);
        Task Edit(ClientDTO client);
        Task Delete(ClientDTO client);
        Task<ClientDTO> Create(ClientDTO client);
        Task<bool> IsClientExist(string email);
        Task Assign(int clientId, int saleId);
    }
}
