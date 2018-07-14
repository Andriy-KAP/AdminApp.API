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
using System.Linq.Expressions;
using System.Data.SqlClient;
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

        public async Task<int> GetUsersCount()
        {
            return await userRepository.GetAll().CountAsync();
        }

        public async Task<int> GetAdminsCount()
        {
            return await userRepository.FindBy(_ => _.Role.Name == "Admin").CountAsync();
        }

        public async Task<int> GetManagersCount()
        {
            return await userRepository.FindBy(_ => _.Role.Name == "Manager").CountAsync();
        }

        public async Task<bool> IsUserExist(string username)
        {
            var user = await userRepository.FindBy(_ => _.Email == username).FirstOrDefaultAsync();
            return (user == null) ? false : true;
        }

        public async Task<UserDTO> Create(UserDTO user)
        {
            var targetUser = mapper.Map<UserDTO, User>(user);
            userRepository.Add(targetUser);
            await SaveChanges();
            var addedUser = await userRepository.FindBy(_ => _.Email == user.Email).ToListAsync();
            //TODO remove twice time database query (add- first, second- findBy)
            return mapper.Map<User, UserDTO>(addedUser.FirstOrDefault());
        }

        public async Task Delete(int id)
        {
            userRepository.Delete(id);
            await SaveChanges();
        }

        public async Task Edit(UserDTO user)
        {
            var targetUser = mapper.Map<UserDTO, User>(user);
            userRepository.Edit(targetUser);
            await SaveChanges();
        }

        public async Task<PaginatedList<UserDTO>> GetUsers(int pageIndex, int pageSize, int? officeId, string search)
        {
            string searchParam = search == null ? string.Empty : search;
            Expression<Func<User, object>> groupIncluding = c => c.Group;
            PaginatedList<User> users = null;
            if(officeId != null)
            {
                users = await userRepository
                    .AllIncluding(groupIncluding)
                    .Where(_=>_.GroupId == officeId)
                    .Where(_ => _.Email.Contains(searchParam) || _.Group.Name.Contains(searchParam) || _.Id.ToString().Contains(searchParam))
                    .ToPaginatedList(pageIndex, pageSize, _ => _.Email);
            }
            else
            {
                users = await userRepository
                    .AllIncluding(groupIncluding)
                    .Where(_ => _.Email.Contains(searchParam) || _.Group.Name.Contains(searchParam) || _.Id.ToString().Contains(searchParam))
                    .ToPaginatedList(pageIndex, pageSize, _ => _.Email);
            }
            var mappedData = mapper.Map<PaginatedList<User>, PaginatedList<UserDTO>>(users);
            return mappedData;
        }

        private async Task SaveChanges()
        {
            await userRepository.Save();
        }
    }
}
