using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Service
{
    public interface IServiceContext
    {
        string ClientIP { get; }
        string ClientID { get; }
        string SessionId { get; }

        string GetValue(string key);

        string GetTempFilePath(string fileGuid);
    }
}
