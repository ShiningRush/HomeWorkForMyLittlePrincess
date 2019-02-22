using Abp.EntityFramework;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.EntityFramework.Repositories
{
    public class OrganizeRepository : TreeRepositoryBase<UserPermissionDbContext, Organize, Guid>, ITreeRepository<Organize,Guid>
    {
        public OrganizeRepository(IDbContextProvider<UserPermissionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
        public override Organize Insert(Organize entity)
        {
            ThereIsOnlyOneTopOrganize(null,entity.ParentId);
            return base.Insert(entity);
        }

        public override Organize Update(Organize entity)
        {
            ThereIsOnlyOneTopOrganize(entity.Id, entity.ParentId);
            return base.Update(entity);
        }

        public override void Delete(Organize entity)
        {
            CanNotDeleteDefaultTopOrganize(entity);
            entity.Code += $"-{Guid.NewGuid().ToString().Substring(0, 8)}";
            entity.Name += "(已删除)";
            base.Delete(entity);
        }

        /// <summary>
        /// 不能删除默认顶级机构
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void CanNotDeleteDefaultTopOrganize(Organize entity)
        {
            if (entity.Id == Guid.Parse(Organize.DEFAULT_TOP_ORGANIZEID))
            {
                throw new CustomHttpException("不能删除默认顶级机构");
            }
        }

      

        /// <summary>
        /// 只能有一个顶级机构
        /// </summary>
        /// <param name="organizeId">机构ID</param>
        /// <param name="parentOrganizeId">上级机构ID</param>
        protected virtual void ThereIsOnlyOneTopOrganize(Guid? organizeId, Guid? parentOrganizeId)
        {
            if (parentOrganizeId.HasValue) return;
            Organize topOrganize;
            if (organizeId.HasValue)
            {
                topOrganize = this.FirstOrDefault(s => !s.ParentId.HasValue && s.Id != organizeId.Value);
            }
            else
            {
                topOrganize = this.FirstOrDefault(s => !s.ParentId.HasValue);
            }
            if(topOrganize != null)
            {
                throw new CustomHttpException("只能有一个顶级机构");
            }
        }

    }
}
