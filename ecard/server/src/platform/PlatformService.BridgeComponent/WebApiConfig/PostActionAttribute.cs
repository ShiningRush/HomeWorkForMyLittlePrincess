using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.WebApiConfig
{
    /// <summary>
    /// Post形式的Api
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PostActionAttribute : Attribute
    {

    }
}
