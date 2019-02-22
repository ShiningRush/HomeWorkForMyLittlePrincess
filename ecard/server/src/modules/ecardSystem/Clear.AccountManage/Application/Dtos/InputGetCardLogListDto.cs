using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    /// <summary>
    /// 卡物日志入参
    /// </summary>
    public class InputGetCardLogListDto: YiBan.Common.Pages.PagerParameter
    {
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 操作类型（卡务类型）
        /// </summary>
        public string OperationType { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IDNO { get; set; }
    }
}
