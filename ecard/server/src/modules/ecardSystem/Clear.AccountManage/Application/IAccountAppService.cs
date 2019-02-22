using PlatformService.BridgeComponent.Enum;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public interface IAccountAppService : IOpenWebApi
    {
        /// <summary>
        /// 获取账户
        /// </summary>
        /// <param name="cardType">卡类型 空默认为就诊卡</param>
        /// <param name="cardNo">卡号</param>
        /// <returns></returns>
        AccountDto GetAccountByCard(string cardType,string cardNo);

        /// <summary>
        /// 获取账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        YiBan.Common.Pages.PagerResult<AccountDto> GetAccounts(GetAccountsInput  input);

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="input"></param>
        string CreateAccount(CreateAccountInput input);

        /// <summary>
        /// 修改账户
        /// </summary>
        /// <param name="input"></param>
        void UpdateAccount(UpdateAccountInput input);

        /// <summary>
        /// 冻结账户
        /// </summary>
        /// <param name="accountId"></param>
        [HttpAction(HttpVerb.Get)]
        void FreezeAccount(string accountId);

        /// <summary>
        /// 解冻账户
        /// </summary>
        /// <param name="accountId"></param>
        [HttpAction(HttpVerb.Get)]
        void ThawAccount(string accountId);

        /// <summary>
        /// 账户充值
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        void AccountRecharge(AccountRechargeInput input);
         
        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        void Refund(AccountDeductionFeeInput input);

        

    }
}
