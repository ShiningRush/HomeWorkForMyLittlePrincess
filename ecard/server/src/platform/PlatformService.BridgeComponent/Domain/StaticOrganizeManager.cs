using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformService.BridgeComponent.Domain.Entities;
using System.Collections.Concurrent;
using Abp.Domain.Entities;

namespace PlatformService.BridgeComponent.Domain
{
    public class StaticOrganizeManager : IStaticOrganizeManager,Abp.Dependency.ISingletonDependency
    {
        private static ConcurrentDictionary<Guid, OrganizeBase> _organizeConcurrentDictionary = new ConcurrentDictionary<Guid, OrganizeBase>();

        public void Add(OrganizeBase organize)
        {
            _organizeConcurrentDictionary.TryAdd(organize.Id, organize);
        }

        public OrganizeBase Get(Guid organizeId)
        {
            OrganizeBase organize;
            if (!_organizeConcurrentDictionary.TryGetValue(organizeId, out organize))
            {
                throw new EntityNotFoundException($"机构{organizeId}不存在");
            }
            return organize;
        }

        public List<OrganizeBase> GetAllChildren(Guid organizeId)
        {
            var treeList = new List<OrganizeBase>() { this.Get(organizeId) };
            foreach (var tree in this.GetList(s=>s.ParentId == organizeId))
            {
                treeList.AddRange(GetAllChildren(tree.Id));
            }
            return treeList;
        }

        public List<OrganizeBase> GetAllParents(Guid organizeId)
        {
            var treeList = new List<OrganizeBase>();
            var tree = this.Get(organizeId);
            treeList.Add(tree);
            if (tree.ParentId.HasValue)
            {
                treeList.AddRange(GetAllParents(tree.ParentId.Value));
            }
            return treeList;
        }

        public List<OrganizeBase> GetList(Func<OrganizeBase, bool> predicate)
        {
            return _organizeConcurrentDictionary.Values.Where(predicate).ToList();
        }

        public void Initialize(List<OrganizeBase> organizes)
        {
            organizes.ForEach((item) => {
                _organizeConcurrentDictionary.TryAdd(item.Id, item);
            });
        }

        public void Remove(OrganizeBase organize)
        {
            OrganizeBase org;
            _organizeConcurrentDictionary.TryRemove(organize.Id, out org);
        }

        public void Update(OrganizeBase organize)
        {
            _organizeConcurrentDictionary.TryUpdate(organize.Id, organize, organize);
        }
    }
}
