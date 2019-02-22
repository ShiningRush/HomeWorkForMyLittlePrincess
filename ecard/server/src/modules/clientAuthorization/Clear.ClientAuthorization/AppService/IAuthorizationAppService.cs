using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.AppService
{
    /// <summary>
    /// 授权信息服务接口
    /// </summary>
    public interface IAuthorizationAppService :  IOpenWebApi
    {
        /// <summary>
        /// 获取所有的授权信息
        /// </summary>
        /// <returns>所有接入应用</returns>
        List<AuthorizationOutputDTO> GetAllApp();
    }
}
