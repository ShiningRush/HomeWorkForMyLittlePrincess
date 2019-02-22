using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    /// <summary>
    /// 获取黑白名单列表（分页）
    /// </summary>
    public class InputGetBlacklistDto: YiBan.Common.Pages.PagerParameter
    {
        /// <summary>
        /// 名单类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 日期类型 0 开始有效日期 1 失效日期
        /// </summary>
        public int DateType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
