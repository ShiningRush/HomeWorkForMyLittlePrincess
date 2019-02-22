using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.WebApiConfig
{
    /// <summary>
    /// 标记不开放的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableWebApiAttribute : Attribute
    {
    }
}
