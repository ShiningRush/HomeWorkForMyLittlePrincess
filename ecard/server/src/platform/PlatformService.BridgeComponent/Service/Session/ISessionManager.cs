using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Service.Session
{
    public interface ISessionManager
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        Guid? UserId { get; }
        /// <summary>
        /// 帐号
        /// </summary>
        string LoginName { get; }
        /// <summary>
        /// 当前机构ID
        /// </summary>
        Guid? CurrentOrganizeId { get; }

        /// <summary>
        /// 支持的机构ID
        /// </summary>
        List<Guid> SupportOrganizeIds { get; }

        SessionInfo GetSessionInfo();

        void SetSessionInfo(string sessionId, SessionInfo info);

        void ClearSessionInfo();
    }
}
