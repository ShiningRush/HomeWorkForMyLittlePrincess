using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Service.Session
{
    public class NullSessionManager : ISessionManager
    {
        public static NullSessionManager Instance { get; } = new NullSessionManager();

        public Guid? UserId => null;
        public string LoginName => null;

        public Guid? CurrentOrganizeId => null;

        public List<Guid> SupportOrganizeIds => new List<Guid>();

        public void ClearSessionInfo()
        {
        }

        public SessionInfo GetSessionInfo()
        {
            return null;
        }

        public void SetSessionInfo(string sessionId, SessionInfo info)
        {
        }
    }
}
