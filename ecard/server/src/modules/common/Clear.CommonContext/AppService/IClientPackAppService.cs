using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService
{
    /// <summary>
    /// 客户端包相关应用服务接口
    /// </summary>
    public interface IClientPackAppService : IOpenWebApi
    {
        /// <summary>
        /// 根据Web前端传回来的IP生成客户端安装包
        /// </summary>
        /// <param name="clientIp">用户访问的ip地址域名</param>
        /// <returns>安装包下载相对路径</returns>
        [DontNeedAuth]
        HttpFileOutput GetClientPackFile(string clientIp);
        /// <summary>
        /// 返回服务器端客户端最新版本号
        /// </summary>
        /// <returns>版本号</returns>
        [DontNeedAuth]
        string GetLatestVersion();
    }
}
