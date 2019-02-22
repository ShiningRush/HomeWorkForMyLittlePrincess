using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    /// <summary>
    /// 外部调用的账户应用服务
    /// </summary>
    public interface IOutsideAccountAppService : IOpenWebApi
    {
        /// <summary>
        /// 账户充值
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        void Recharge(OutsideRechargeInput input);

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        void Refund(OutsideRefundInput input);

        /// <summary>
        /// 扣费
        /// </summary>
        void DeductionFee(OutsideDeductionFeeInput input);

        /// <summary>
        /// 获取账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutsideAccountDto GetAccountByCard(OutsideCardInput input);

        /// <summary>
        /// 发卡
        /// </summary>
        /// <param name="input"></param>
        string CreditCard(OutsideCreditCardInput input);
    }
}
