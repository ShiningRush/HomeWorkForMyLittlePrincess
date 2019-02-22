using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.EntityFramework.Repositories
{
    public class UserRepository : EfRepositoryBase<UserPermissionDbContext, User, Guid>,IRepository<User, Guid>
    {
        public UserRepository(IDbContextProvider<UserPermissionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public override void Delete(User entity)
        {
            CanNotDeleteOwnUser(entity);
            CanNotDeleteDefaultSystemUser(entity);
            entity.LoginName += $"-{Guid.NewGuid().ToString().Substring(0, 8)}";
            entity.UserName += "(已删除)";
            base.Delete(entity);
        }

        /// <summary>
        /// 不能删除用户自身
        /// </summary>
        protected virtual void CanNotDeleteOwnUser(User entity)
        {
            if(Context.SessionManager.UserId.HasValue && Context.SessionManager.UserId.Value.Equals(entity.Id))
            {
                throw new CustomHttpException("不能删除用户自身");
            }
        }

        /// <summary>
        /// 不能删除默认管理员用户
        /// </summary>
        protected virtual void CanNotDeleteDefaultSystemUser(User entity)
        {
            if (entity.Id.Equals(Guid.Parse(User.SYSTEM_USERID)))
            {
                throw new CustomHttpException("不能删除默认管理员用户");
            }
        }

    }
}
