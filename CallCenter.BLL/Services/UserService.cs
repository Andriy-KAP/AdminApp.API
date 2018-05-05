﻿using CallCenter.BLL.Core;
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

        public async Task<UserDTO> Create(UserDTO user)
        {
            var targetUser = mapper.Map<UserDTO, User>(user);
            userRepository.Add(targetUser);
            await SaveChanges();
            var addedUser = await userRepository.FindBy(_ => _.Email == user.Email).ToListAsync();

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

        public async Task<PaginatedList<UserDTO>> GetUsers(int pageIndex, int pageSize, int officeId)
        {
            Expression<Func<User, object>> groupIncluding = c => c.Group;
            //Expression<Func<User, object>> filter1 = c => c.Group.Sales;
            //Expression<Func<User, object>> filter2 = c => c.Sales;
            var users = await userRepository.AllIncluding(groupIncluding).Where(_=>_.GroupId == officeId).ToPaginatedList(pageIndex, pageSize , _=>_.Email);
            var mappedData = mapper.Map<PaginatedList<User>, PaginatedList<UserDTO>>(users);
            return mappedData;
        }

        private async Task SaveChanges()
        {
            await userRepository.Save();
        }
    }
}
