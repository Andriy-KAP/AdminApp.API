using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Extensions
{
    public static class IdentityRepositoryExtensions
    {
        
        public static async Task<IdentityResult> RegisterUser(this IEntityRepository<ApplicationUser> identityRepository, UserManager<ApplicationUser> userManager, CallCenter.DAL.Models.UserDomain userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public static async Task<ApplicationUser> FindUser(this IEntityRepository<ApplicationUser> identityRepository, UserManager<ApplicationUser> userManager, string email)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);

            return user;
        }

        public static IQueryable<ApplicationUser> GetUsers(this IEntityRepository<ApplicationUser> identityRepository, UserManager<ApplicationUser> userManager)
        {
            return userManager.Users;
        }

        public static async Task<IdentityResult> DeleteUser(this IEntityRepository<ApplicationUser> identityRepository, UserManager<ApplicationUser> userManager, string email)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if(user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                return result;
            }
            return new IdentityResult("Error during remove user.");
            
        }

        public static async Task<IdentityResult> EditUser(this IEntityRepository<ApplicationUser> identityRepository, UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            ApplicationUser targetUser = await userManager.FindByIdAsync(user.Id);

            targetUser.Email = user.Email;
            targetUser.PhoneNumber = user.PhoneNumber;
            targetUser.UserName = user.UserName;

            if(user != null)
            {
                IdentityResult result = await userManager.UpdateAsync(targetUser);
                return result;
            }
            return new IdentityResult("Error during update user");
        }
    }
}
