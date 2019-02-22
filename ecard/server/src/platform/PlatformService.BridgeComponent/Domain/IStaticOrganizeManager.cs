using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain
{
    public interface IStaticOrganizeManager
    {
        void Initialize(List<OrganizeBase> organizes);

        void Add(OrganizeBase organize);

        void Update(OrganizeBase organize);

        void Remove(OrganizeBase organize);

        /// <summary>
        /// 获取所有父节点
        /// </summary>
        /// <param name="treeId"></param>
        /// <returns></returns>
        List<OrganizeBase> GetAllParents(Guid organizeId);

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <param name="treeId"></param>
        /// <returns></returns>
        List<OrganizeBase> GetAllChildren(Guid organizeId);

        /// <summary>
        /// 获取机构
        /// </summary>
        /// <param name="treeId"></param>
        /// <returns></returns>
        OrganizeBase Get(Guid organizeId);

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <param name="treeId"></param>
        /// <returns></returns>
        List<OrganizeBase> GetList(Func<OrganizeBase, bool> predicate);
    }
}
