using Abp.Runtime.Caching;
using PlatformService.BridgeComponent.Domain;
using PlatformService.Core.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Service.Session
{
    public class SessionManager : ISessionManager, Abp.Dependency.ITransientDependency
    {
        private readonly IStaticOrganizeManager _staticOrganizeManager;
        private readonly ICacheManager _cacheManager;
        private readonly IServiceContext _serviceContext;


        public SessionManager(ICacheManager cacheManager, IServiceContext serviceContext,IStaticOrganizeManager staticOrganizeManager)
        {
            _cacheManager = cacheManager;
            _serviceContext = serviceContext;
            _staticOrganizeManager = staticOrganizeManager;
        }

        public void ClearSessionInfo()
        {
            if (_serviceContext.ClientID == null)
            {
                return;
            }
            _cacheManager.GetCache(CacheConst.SESSIONINFO_CACHE).Remove(_serviceContext.ClientID);
        }

        public SessionInfo GetSessionInfo()
        {
            if (_serviceContext.ClientID == null)
            {
                return null;
            }
            return (SessionInfo)_cacheManager.GetCache(CacheConst.SESSIONINFO_CACHE).GetOrDefault(_serviceContext.ClientID);
        }

        public void SetSessionInfo(string sessionId, SessionInfo info)
        {
            _cacheManager.GetCache(CacheConst.SESSIONINFO_CACHE).Set(sessionId, info, TimeSpan.FromMinutes(20));
        }

        public Guid? UserId => GetSessionInfo()?.UserId;

        public string LoginName => GetSessionInfo()?.LoginName;

        public Guid? CurrentOrganizeId => GetSessionInfo()?.CurrentOrganizeId;

        public List<Guid> SupportOrganizeIds {
            get
            {
                if(CurrentOrganizeId.HasValue)
                {
                    return _staticOrganizeManager.GetAllChildren(CurrentOrganizeId.Value).Select(s=>s.Id).ToList();
                }
                else
                {
                    return new List<Guid>();
                }
            }
        }

    }
}
