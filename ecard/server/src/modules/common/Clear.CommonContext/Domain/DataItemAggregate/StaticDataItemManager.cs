using Abp.Runtime.Caching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Domain.DataItemAggregate
{
    public class StaticDataItemManager : IStaticDataItemManager,Abp.Dependency.ITransientDependency
    {
        private const string DATAITEMDTO_CACHE_NAME = "DATAITEMDTO";


        private readonly ICacheManager _cacheManager;
        public StaticDataItemManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        private ITypedCache<string, ConcurrentDictionary<string, string>> GetCache()
        {
            return _cacheManager.GetCache(DATAITEMDTO_CACHE_NAME).AsTyped<string, ConcurrentDictionary<string, string>>();
        }

        public void Add(DataItemDto dataItemDto)
        {
            Update(dataItemDto);
        }

        public DataItemDto Get(string category, string key)
        {
            string value;
            var cacheValue = GetCache().GetOrDefault(category);
            if (cacheValue == null) return null;

            if (cacheValue.TryGetValue(key,out value))
            {
                return new DataItemDto(category, key, value);
            }
            return null;
        }

        public List<DataItemDto> GetList(string category)
        {
            List<DataItemDto> list = new List<DataItemDto>();
            var cacheValue = GetCache().GetOrDefault(category);
            if (cacheValue == null) return list;

            foreach (var item in cacheValue)
            {
                list.Add(new DataItemDto(category, item.Key, item.Value));
            }

            return list;
        }

        public void Add(List<DataItemDto> dataItemDtos)
        {
            foreach (var dataItemDto in dataItemDtos)
            {
                Add(dataItemDto);
            }
        }

        public void Remove(DataItemDto dataItemDto)
        {
            string value;
            var cacheValue = GetCache().GetOrDefault(dataItemDto.Category);
            if (cacheValue == null) return ;

            if (cacheValue.TryRemove(dataItemDto.Key, out value))
            {
            }
        }

        public void Update(DataItemDto dataItemDto)
        {
            var cache = GetCache();
            var cacheValue = cache.Get(dataItemDto.Category, category => {
                return new ConcurrentDictionary<string, string>();
            });
            cacheValue.AddOrUpdate(dataItemDto.Key, dataItemDto.Value, (key, oldValue) => dataItemDto.Value);
            cache.Set(dataItemDto.Category, cacheValue);
        }
    }
}
