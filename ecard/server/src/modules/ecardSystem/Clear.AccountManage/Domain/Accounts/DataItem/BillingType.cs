using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts.DataItem
{
    /// <summary>
    /// 交易类型
    /// </summary>
    [Description("交易类型")]
    public class BillingType : Enumeration
    {

        public static BillingType Recharge = new BillingType(nameof(Recharge), "充值");
        public static BillingType Consumption = new BillingType(nameof(Consumption), "消费");
        public static BillingType Refund = new BillingType(nameof(Refund), "退费");

        public override string Category => nameof(BillingType);
    
        protected BillingType(string key, string value) : base(key, value)
        {
        }
    }
}
