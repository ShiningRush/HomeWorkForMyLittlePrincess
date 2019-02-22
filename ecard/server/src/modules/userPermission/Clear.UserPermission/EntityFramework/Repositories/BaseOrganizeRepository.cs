using Abp.EntityFramework;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PlatformService.BridgeComponent.Domain;
using Abp.Domain.Uow;

namespace Clear.UserPermission.EntityFramework.Repositories
{

    public class BaseOrganizeRepository : TreeRepositoryBase<UserPermissionDbContext, OrganizeBase,Guid>, ITreeRepository<OrganizeBase,Guid>
    {

        public BaseOrganizeRepository(IDbContextProvider<UserPermissionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public override IQueryable<OrganizeBase> GetAll()
        {
            return Context.Set<Organize>().AsQueryable<OrganizeBase>();
        }

    }
}
