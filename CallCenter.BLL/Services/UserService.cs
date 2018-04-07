using CallCenter.BLL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.DAL.Models;
using CallCenter.DAL.Core;
using System.Data.Entity;
using CallCenter.BLL.DTO;
using AutoMapper;
using CallCenter.DAL.Extensions;

namespace CallCenter.BLL.Services
{
    public class UserService : IUserService
    {
        private IEntityRepository<User> userRepository;
        private IMapper mapper;

        public UserService(IEntityRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task Create(User user)
        {
            userRepository.Add(user);
            await SaveChanges();
        }

        public async Task Delete(int id)
        {
            userRepository.Delete(id);
            await SaveChanges();
        }

        public async Task Edit(User user)
        {
            userRepository.Edit(user);
            await SaveChanges();
        }

        public async Task<PaginatedList<UserDTO>> GetUsers(int pageIndex, int pageSize)
        {
            var users = await userRepository.GetAll().ToPaginatedList(pageIndex, pageSize , _=>_.Email);
            var mappedData = mapper.Map<PaginatedList<User>, PaginatedList<UserDTO>>(users);
            return mappedData;
        }

        private async Task SaveChanges()
        {
            await userRepository.Save();
        }
    }
}
