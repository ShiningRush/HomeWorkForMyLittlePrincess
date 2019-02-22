using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Authorization
{

    /// <summary>
    /// OAuth2授权帮助接口
    /// </summary>
    public interface IOAuth2Authorization
    {
        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        bool CheckAuthentication(string id,string secret);

        /// <summary>
        /// 检查方法授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        bool CheckMethodAuthorization(string id, string method);
    }
}
