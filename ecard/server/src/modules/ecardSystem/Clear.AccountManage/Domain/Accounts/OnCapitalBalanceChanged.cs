using Abp;
using Clear.AccountManage.Domain.Accounts.DataItem;
using JetBrains.Annotations;
using ServiceAnt.Subscription;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 资金余额变更后增加账单记录领域事件
    /// </summary>
    public class OnCapitalBalanceChanged : IEventTrigger
    {
        /// <summary>
        /// 账号代码
        /// </summary> 
        public  string AccountId { get; private set; }

        /// <summary>
        /// 资金类型 对应数据字典表 CapitalType 
        /// </summary> 
        public string CapitalType { get; private set; }
        /// <summary>
        /// 金额
        /// </summary> 
        public  decimal Balance { get; private set; }
        
        /// <summary>
        /// 变更金额
        /// </summary>
        public decimal ChangedAmount { get; private set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; private set; }

        /// <summary>
        /// 消费类型
        /// </summary>
        public string BillingType { get; private set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; private set; }

        public OnCapitalBalanceChanged([NotNull] Capital capital,[NotNull] BillingType billingType, decimal changedAmount, string payType,string desc):base()
        {
            Check.NotNull(capital, nameof(capital));
            Check.NotNullOrWhiteSpace(capital.AccountId, nameof(AccountId));
            Check.NotNullOrWhiteSpace(capital.CapitalType, nameof(CapitalType));
            Check.NotNull(billingType, nameof(BillingType));

            AccountId = capital.AccountId;
            CapitalType = capital.CapitalType;
            Balance = capital.Balance;
            ChangedAmount = changedAmount;
            PayType = payType;
            Desc = desc;
            BillingType = billingType.Key;
        }

        protected OnCapitalBalanceChanged():base() { }
    }
}
