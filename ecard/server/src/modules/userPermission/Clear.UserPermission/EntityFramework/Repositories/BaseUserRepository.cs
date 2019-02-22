using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.EntityFramework.Repositories
{
    

    public class BaseUserRepository : EfRepositoryBase<UserPermissionDbContext, UserBase, Guid>, IRepository<UserBase,Guid>
    {
        public BaseUserRepository(IDbContextProvider<UserPermissionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public override IQueryable<UserBase> GetAll()
        {
            //return Context.Set<User>().Select(s => new UserBase() { Id = s.Id, LoginName = s.LoginName, OrganizeId = s.OrganizeId, UserName = s.UserName });
            return Context.Set<User>().AsQueryable<UserBase>();
        }
    }
}
