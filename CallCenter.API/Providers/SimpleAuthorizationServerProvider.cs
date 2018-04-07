using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CallCenter.API.Providers
{
    public class SimpleAuthorizationServerProvider //: OAuthAuthorizationServerProvider
    {
        //private UserManager<ApplicationUser> userManager;

        //public SimpleAuthorizationServerProvider()
        //{
        //    this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CallCenterContext()));
        //}

        //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();
        //}

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    //TODO Add CORS   
        //    //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
        //    ApplicationUser user = userManager.Find(context.UserName, context.Password);//await identityRepository.FindUser(userManager, context.UserName, context.Password);
        //    //var t = context.Response.Cookies = 
        //    if (user == null)
        //    {
        //        context.SetError("invalid_grant", "The user name or password is incorrect.");
        //        return;
        //    }
            
        //    //TODO Here is claims had created/ Check user claim in controller (user role)
        //    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    identity.AddClaim(new Claim("username", user.UserName));
        //    identity.AddClaim(new Claim("role", "user"));
        //    identity.AddClaim(new Claim("group", user.Group.Name));
        //    identity.AddClaim(new Claim("groupId", user.GroupId.ToString()));

        //    context.Validated(identity);

        //}
    }
}