using PlatformService.BridgeComponent.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.CustomException
{
    public class NeedPostHttpException : CustomHttpException
    { 
        public NeedPostHttpException(string errMsg) : base(HttpResponseCode.NeedPost, errMsg) { }
    }
}
