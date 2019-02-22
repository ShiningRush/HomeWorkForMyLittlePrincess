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
    /// 卡类型
    /// </summary>
    [Description("卡类型")]
    public class CardType : Enumeration
    {
        public static CardType MedicalCard = new CardType(nameof(MedicalCard), "诊疗卡");

        public override string Category => nameof(CardType);

        protected CardType(string key, string value) : base(key, value)
        {
        }

        public static CardType Default { get { return MedicalCard; } }
    }
}
