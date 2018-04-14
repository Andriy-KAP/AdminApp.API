using AutoMapper;
using CallCenter.API.Models;
using CallCenter.API.Providers;
using CallCenter.BLL.Core;
using CallCenter.BLL.DTO;
using CallCenter.BLL.Infrastructure;
using CallCenter.BLL.Services;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Web.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;

namespace CallCenter.API.App_Start
{
    public class NinjectConfig
    {
        public  static StandardKernel kernelInstance { get; set; }

        static NinjectConfig()
        {
            kernelInstance = new StandardKernel();
        }

        public static IKernel CreateKernel()
        {
            //StandardKernel kernel = new StandardKernel();

            //Load modules from bll layer
            kernelInstance.Load(new BllNinjectModule());

            //kernel.Bind<DefaultModelValidatorProviders>().ToConstant(
            //    new DefaultModelValidatorProviders(
            //        GlobalConfiguration.Configuration.Services.GetServices(
            //            typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()));

            kernelInstance.Bind<IMapper>().ToMethod(AutoMapper).InSingletonScope();

            //Register services
            RegisterServices(kernelInstance);

            return kernelInstance;
        }

        private static IMapper AutoMapper(Ninject.Activation.IContext context)
        {
            Mapper.Initialize(c => {
                c.CreateMap<User, UserDTO>(MemberList.None)
                    .ForMember(_=>_.GroupName, group=>group.MapFrom(_=>_.Group.Name));
                c.CreateMap<UserDTO, User>(MemberList.None);
                c.CreateMap<UserDTO, UserModel>(MemberList.None);
                c.CreateMap<UserModel, UserDTO>(MemberList.None);
                c.CreateMap<GroupModel, GroupDTO>(MemberList.None);
                c.CreateMap<GroupDTO, Group>(MemberList.None);
                c.CreateMap<IQueryable<Group>, IQueryable<GroupDTO>>(MemberList.None);
                c.CreateMap<InfoDTO, InfoModel>(MemberList.None);
                c.CreateMap<PaginatedList<User>, PaginatedList<UserDTO>>(MemberList.None)
                    .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));
                c.CreateMap<PaginatedList<UserDTO>, PaginatedList<UserModel>>(MemberList.None)
                    .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));
            });
            

            return Mapper.Instance;
        }
            private static void RegisterServices(KernelBase kernel)
        {
            kernel.Bind<SimpleAuthorizationServerProvider>().To<SimpleAuthorizationServerProvider>();
            kernel.Bind<IGroupService>().To<GroupService>();
            kernel.Bind<IAuthService>().To<AuthService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IInfoService>().To<InfoService>();
        }
    }
}