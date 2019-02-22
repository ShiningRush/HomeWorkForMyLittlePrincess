using PlatformService.BridgeComponent.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.CustomException
{
    public class NeedGetHttpException : CustomHttpException
    {
        public NeedGetHttpException(string errMsg) : base(HttpResponseCode.NeedGet, errMsg) { }
    }
}
