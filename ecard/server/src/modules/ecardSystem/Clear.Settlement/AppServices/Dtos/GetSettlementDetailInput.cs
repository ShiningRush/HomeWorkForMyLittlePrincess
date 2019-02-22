using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.Settlement.AppServices.Dtos
{
    public class GetSettlementListInput : PagerParameter
    {
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
