using PlatformService.BridgeComponent.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.WebApiConfig
{
    /// <summary>
    /// 指定WebApi的Http方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpActionAttribute : Attribute
    {
        public HttpVerb HttpMethod { get; set; }

        public HttpActionAttribute(HttpVerb httpMethod)
        {
            HttpMethod = httpMethod;
        }
    }
}
