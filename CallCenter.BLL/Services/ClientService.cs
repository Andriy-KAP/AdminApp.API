using AutoMapper;
using CallCenter.BLL.Core;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.BLL.DTO;
using System.Linq.Expressions;
using CallCenter.DAL.Extensions;
using System.Data.Entity;

namespace CallCenter.BLL.Services
{
    public class ClientService: IClientService
    {
        private IEntityRepository<Client> clientRepository;
        private IMapper mapper;

        public ClientService(IEntityRepository<Client> clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        public async Task Assign(int clientId, int saleId)
        {
            var targetClient = await clientRepository.FindBy(_ => _.Id == clientId).FirstOrDefaultAsync();
            targetClient.SaleId = saleId;
            clientRepository.Edit(targetClient);
            await Save();
        }

        public async Task<bool> IsClientExist(string email)
        {
            var client = await clientRepository.FindBy(_ => _.Email == email).FirstOrDefaultAsync();
            return (client == null) ? false : true;
        }

        public async Task<ClientDTO> Create(ClientDTO clientDTO)
        {
            var client = mapper.Map<ClientDTO, Client>(clientDTO);
            clientRepository.Add(client);
            await Save();

            return clientDTO;
        }

        public async Task Delete(ClientDTO client)
        {
            clientRepository.Delete(client.Id);
            await Save();
        }

        public async Task Edit(ClientDTO clientDTO)
        {
            var client = mapper.Map<ClientDTO, Client>(clientDTO);
            clientRepository.Edit(client);
            await Save();
        }

        //This method return empty collection because of saleIncluding is using
        public async Task<PaginatedList<ClientDTO>> GetClients(int pageIndex, int pageSize, int? groupId, string search)
        {
            string searchParam = search == null ? string.Empty : search;
            PaginatedList<Client> clients= null;
            Expression<Func<Client, object>> saleIncluding = s => s.Sale;

            if(groupId != null)
            {
                clients = await clientRepository.AllIncluding(saleIncluding)
                    .Where(_=>_.Sale.GroupId == groupId)
                    .Where(_ => _.Name.Contains(searchParam))
                    .ToPaginatedList(pageIndex, pageSize, _=>_.Name);
            }
            else
            {
                clients = await clientRepository.AllIncluding(saleIncluding)
                    .Where(_ => _.Name.Contains(searchParam))
                    .ToPaginatedList(pageIndex, pageSize, _ => _.Name);
            }

            var mappedData = mapper.Map<PaginatedList<Client>, PaginatedList<ClientDTO>>(clients);
            return mappedData;
        }

        private async Task Save()
        {
            await clientRepository.Save();
        }
    }
}
