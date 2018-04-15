﻿using CallCenter.BLL.DTO;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.BLL.Core
{
    public interface IUserService
    {
        Task<PaginatedList<UserDTO>> GetUsers(int pageIndex, int pageSize);
        Task<UserDTO> Create(UserDTO user);
        Task Edit(UserDTO user);
        Task Delete(int id);
    }
}
