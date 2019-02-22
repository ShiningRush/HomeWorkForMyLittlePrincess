using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformService.BridgeComponent.WebApiConfig;
namespace Clear.AccountManage.Application
{
    public interface IBlacklistAppService : IOpenWebApi
    {
        /// <summary>
        /// 获取黑白名单（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        YiBan.Common.Pages.PagerResult<OutputGetBlacklistDto> GetBlacklist(InputGetBlacklistDto input);
        /// <summary>
        /// 添加黑白名单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string AddBlacklist(InputAddBlacklistDto input);
        /// <summary>
        /// 修改黑白名单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void UpdateBlacklist(InputUpdateBlacklistDto input);
        /// <summary>
        /// 删除黑白名单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteBlacklist(Guid id);
    }
}
