using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Authorization
{
    public interface ISSOAuthorization
    {
        bool CheckTicket(string token, string userName);
    }
}
