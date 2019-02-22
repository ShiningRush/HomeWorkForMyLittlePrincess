using Clear.Settlement.AppServices.Dtos;
using PlatformService.BridgeComponent.Enum;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.Settlement.AppServices
{
    /// <summary>
    /// 结算相关应用服务
    /// </summary>
    public interface ISettlementAppService : IOpenWebApi
    {
        /// <summary>
        /// 按时间段结算交易记录
        /// </summary>
        /// <param name="inputDto"></param>
        void SettleBillingRecord(SettleBillingRecordInput inputDto);

        /// <summary>
        /// 获取结算明细
        /// </summary>
        PagerResult<GetSettlementListOutput> GetSettlementList(GetSettlementListInput inputDto);

        /// <summary>
        /// 获取单次结算的交易记录
        /// </summary>
        List<GetSingleSettlementDetailOutput> GetSingleSettlementDetail(GetSingleSettlementDetailInput inputDto);

        /// <summary>
        /// 导出单次结算的交易记录
        /// </summary>
        [DontNeedAuth]
        [HttpAction(HttpVerb.Get)]
        HttpFileOutput ExportSingleSettlementDetail(ExportSingleSettlementDetailInput inputDto);

        /// <summary>
        /// 获取当前未结算的交易记录图表集
        /// </summary>
        /// <param name="inputDto"></param>
        List<GetNotSettledBillingGraphDataOutput> GetNotSettledBillingGraphData(GetNotSettledBillingGraphDataInput inputDto);
    }
}
