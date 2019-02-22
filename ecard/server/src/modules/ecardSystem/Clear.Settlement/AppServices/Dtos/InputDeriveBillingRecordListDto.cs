using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public class InputDeriveBillingRecordListDto
    {
        public string CardNo { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 应用场景
        /// </summary>
        public string ApplicationScene { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }
    }
}
