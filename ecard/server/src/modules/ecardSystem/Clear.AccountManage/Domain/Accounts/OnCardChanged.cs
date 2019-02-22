using Abp.Events.Bus;
using Clear.AccountManage.Domain.Accounts.DataItem;
using JetBrains.Annotations;
using PlatformService.BridgeComponent.Domain.Entities;
using ServiceAnt.Handler;
using ServiceAnt.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 卡更改记录卡务日志
    /// </summary>
    public class OnCardChanged : IEventTrigger
    {
        /// <summary>
        /// 所属的账户ID
        /// </summary>
        public string AccountId { get;  set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get;  set; }

        /// <summary>
        /// 卡号 
        /// </summary>
        public string CardNo { get;  set; }

        public string CardOperationType { get; set; }

        public string Remark { get; set; }

        public OnCardChanged([NotNull] Card card, [NotNull] CardOperationType cardOperationType, string remark =  null):base()
        {
            AccountId = card.AccountId;
            CardType = card.CardType;
            CardNo = card.CardNo;
            CardOperationType = cardOperationType.Key;
            Remark = remark;
        }

        public OnCardChanged():base() { }
    }
}
