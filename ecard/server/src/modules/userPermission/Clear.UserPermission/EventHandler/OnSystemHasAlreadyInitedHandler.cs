using Abp.Dependency;
using Abp.Domain.Repositories;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.Domain;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.EntityFramework;
using ServiceAnt.Handler.Subscription.Handler;
using ServiceAnt.Subscription.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.EventHandler
{
    public class OnSystemHasAlreadyInitedHandler : IEventHandler<OnDbHasAlreadyInited>, ITransientDependency
    {
        private readonly IRepository<Organize, Guid> _organizeRepository;
        private readonly IStaticOrganizeManager _staticOrganizeManager;


        public OnSystemHasAlreadyInitedHandler(
            IRepository<Organize, Guid> organizeRepository, 
            IStaticOrganizeManager staticOrganizeManager)
        {
            _organizeRepository = organizeRepository;
            _staticOrganizeManager = staticOrganizeManager;
        }

        public Task HandleAsync(OnDbHasAlreadyInited param)
        {
            _staticOrganizeManager.Initialize(_organizeRepository.GetAllList().AsEnumerable<OrganizeBase>().ToList());

            return Task.FromResult(0);
        }
    }
}
