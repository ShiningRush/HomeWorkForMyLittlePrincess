using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformService.BridgeComponent.WebApiConfig;
namespace Clear.AccountManage.Application
{
    public interface ICardLogAppService : IOpenWebApi
    {
        /// <summary>
        /// 获取卡务日志列表（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        YiBan.Common.Pages.PagerResult<OutputGetCardLogListDto> GetCardLogList(InputGetCardLogListDto input);
    }
}
