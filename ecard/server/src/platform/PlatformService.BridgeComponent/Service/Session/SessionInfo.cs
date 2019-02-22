using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Service.Session
{
    public class SessionInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }

        public Guid CurrentOrganizeId { get; set; }

    }


}
