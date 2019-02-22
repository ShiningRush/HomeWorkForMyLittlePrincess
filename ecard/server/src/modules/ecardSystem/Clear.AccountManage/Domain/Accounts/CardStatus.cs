using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    public enum CardStatus : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 挂失
        /// </summary>
        [Description("挂失")]
        Loss = 1,

    }
}
