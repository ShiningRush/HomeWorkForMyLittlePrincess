using Abp.Domain.Values;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts.DataItem
{
    [Description("卡操作类型")]
    public class CardOperationType : Enumeration
    {

        public static CardOperationType BindingCard = new CardOperationType(nameof(BindingCard), "绑卡");
        public static CardOperationType LossCard = new CardOperationType(nameof(LossCard), "挂失卡");
        public static CardOperationType ReleaseLoss = new CardOperationType(nameof(ReleaseLoss), "解挂卡");
        public static CardOperationType CancellationCard = new CardOperationType(nameof(CancellationCard), "注销卡");
       
        public override string Category => nameof(CardOperationType);

        protected CardOperationType(string key, string value) : base(key, value)
        {
        }
    }
}
