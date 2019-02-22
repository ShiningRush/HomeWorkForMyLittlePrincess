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
    /// 资金类型
    /// </summary>
    [Description("资金类型")]
    public class CapitalType : Enumeration
    {
        public static CapitalType Universal = new CapitalType(nameof(Universal), "通用资金");

        public override string Category => nameof(CapitalType);

        protected CapitalType(string key, string value) : base(key, value)
        {
        }

        public static CapitalType Default { get { return Universal; } }
    }
}
