using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 账户状态
    /// </summary>
    public enum AccountStatus:byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 冻结
        /// </summary>
        [Description("冻结")]
        Freeze = 1,

    }
}
