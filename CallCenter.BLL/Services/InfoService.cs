using CallCenter.BLL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using System.Data.Entity;

namespace CallCenter.BLL.Services
{
    public class InfoService : IInfoService
    {
        private IEntityRepository<User> userRepository;
        private IEntityRepository<Group> groupRepository;
        private IEntityRepository<Sale> saleRepository;

        public InfoService(IEntityRepository<User> userRepository, 
            IEntityRepository<Group> groupRepository,
            IEntityRepository<Sale> saleRepository)
        {
            this.userRepository = userRepository;
            this.groupRepository = groupRepository;
            this.saleRepository = saleRepository;
        }

        public async Task<InfoDTO> GetInfo()
        {
            var users = await userRepository.GetAll().ToListAsync();
            var groups = await groupRepository.GetAll().ToListAsync();
            var sales = await saleRepository.GetAll().ToListAsync();

            return new InfoDTO
            {
                UsersCount = users.Count,
                GroupsCount= groups.Count,
                SalesCount = sales.Count
            };
        }
    }
}
