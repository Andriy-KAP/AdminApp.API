using CallCenter.BLL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CallCenter.DAL.Core;
using CallCenter.DAL.Extensions;
using CallCenter.DAL.Models;
using AutoMapper;
using CallCenter.BLL.DTO;
using System.Data.Entity;

namespace CallCenter.BLL.Services
{
    public class IdentityService : IIdentityService, IDisposable
    {
        private IEntityRepository<ApplicationUser> identityRepository;
        private UserManager<ApplicationUser> userManager;
        private AutoMapper.IMapper mapper;

        public IdentityService(IEntityRepository<ApplicationUser> identityRepository, AutoMapper.IMapper mapper)
        {
            this.identityRepository = identityRepository;
            this.mapper = mapper;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(identityRepository.GetContext()));
        }

        public async Task<UserDTO> FindUser(string email)
        {
            ApplicationUser targetUser = await identityRepository.FindUser(userManager, email);
            UserDTO user = mapper.Map<ApplicationUser, UserDTO>(targetUser);

            return user;
        }

        public async Task<IdentityResult> RegisterUser(CallCenter.DAL.Models.UserDomain user)
        {
            return await identityRepository.RegisterUser(userManager, user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            IEnumerable<ApplicationUser> users = await (identityRepository.GetUsers(userManager)).ToListAsync();
            IEnumerable<UserDTO> usersDto = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDTO>>(users);

            return usersDto;
        }

        public async Task<IdentityResult> RemoveUser(string email)
        {
            IdentityResult result = await identityRepository.DeleteUser(userManager, email);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(IdentityDTO user)
        {
            ApplicationUser targetUser = mapper.Map<IdentityDTO, ApplicationUser>(user);
            IdentityResult result = await identityRepository.EditUser(userManager, targetUser);
            identityRepository.Save();
            return result;
        }

        public void Dispose()
        {
            identityRepository.Dispose();
            userManager.Dispose();
        }
    }
}
