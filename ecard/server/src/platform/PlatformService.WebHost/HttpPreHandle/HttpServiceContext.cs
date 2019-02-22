using Abp.Dependency;
using PlatformService.BridgeComponent.Service;
using PlatformService.Core.Common.Const;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Yiban.CoreService.Host.Http
{
    public class HttpServiceContext : IServiceContext, ITransientDependency
    {
        public string ClientIP => GetValue(HttpContextConst.HTTP_CLIENT_IP);

        public string ClientID => GetValue(HttpContextConst.HTTP_CLIENT_ID);

        public string SessionId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public HttpServiceContext()
        {
        }

        public string GetValue(string key)
        {
            var request = CallContext.LogicalGetData(HttpContextConst.HTTP_REQUEST_CONTEXT) as Dictionary<string, IEnumerable<string>>;
            if (request == null)
            {
                return null;
            }
            IEnumerable<string> result;
            if (request.TryGetValue(key,out result))
            {
                return result.FirstOrDefault();
            }

            return null;
        }

        public string GetTempFilePath(string fileGuid)
        {
            var filePath = string.Format(@"{0}{1}\{2}",
                AppDomain.CurrentDomain.BaseDirectory,
                PlatformServiceConst.TEMP_FILE_NAME,
                fileGuid);

            return Directory.GetFiles(filePath).First();
        }
    }
}
