using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.AppServices.Dtos
{
    public class GetSettlementListOutput
    {
        /// <summary>
        /// 结算记录Id
        /// </summary>
        public string SettlementRecordId { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// 结算内容(Json格式, 需要转化)
        /// </summary>
        public string SettlementContent { get; set; }
    }
}
