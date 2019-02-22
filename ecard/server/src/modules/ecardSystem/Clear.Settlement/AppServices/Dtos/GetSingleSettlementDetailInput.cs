using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.AppServices.Dtos
{
    /// <summary>
    /// 获取单次结算记录详细Input
    /// </summary>
    public class GetSingleSettlementDetailInput
    {
        /// <summary>
        /// 结算记录Id
        /// </summary>
        public Guid SettlementId { get; set; }
    }
}
