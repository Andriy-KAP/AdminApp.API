using CallCenter.BLL.Core;
using CallCenter.BLL.Services;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using Ninject.Modules;

namespace CallCenter.BLL.Infrastructure
{
    public class BllNinjectModule: NinjectModule
    {
        public override void Load()
        {
            Bind<CallCenterContext>().To<CallCenterContext>();
            Bind<IEntityRepository<Group>>().To<EntityRepository<Group>>();
            Bind<IEntityRepository<User>>().To<EntityRepository<User>>();
            Bind<IEntityRepository<Sale>>().To<EntityRepository<Sale>>();
            Bind<ICryptoService>().To<CryptoService>();
        }
    }
}
