using PlatformService.BridgeComponent.Enum;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public interface IBillingRecordAppService : IOpenWebApi
    {
        /// <summary>
        /// 获取用卡明细列表（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        YiBan.Common.Pages.PagerResult<OutputBillingRecordListDto> GetBillingRecordList(InputBillingRecordListDto input);
        /// <summary>
        /// 导出用卡明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DontNeedAuth]
        [HttpAction(HttpVerb.Get)]
        HttpFileOutput DeriveBillingRecordList(InputBillingRecordListDto input);
    }
}
