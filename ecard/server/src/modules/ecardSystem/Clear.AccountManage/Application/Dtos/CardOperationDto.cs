using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    /// <summary>
    /// 卡操作dto
    /// </summary>
    public class CardOperationDto
    {
        /// <summary>
        /// 账户Id
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
    }
}
