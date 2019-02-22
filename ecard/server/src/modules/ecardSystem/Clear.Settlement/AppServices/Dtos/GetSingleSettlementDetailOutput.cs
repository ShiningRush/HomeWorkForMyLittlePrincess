using Clear.Component.Interfaces.Componets.DataFormat;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.AppServices.Dtos
{
    public class GetSingleSettlementDetailOutput
    {
        /// <summary>
        /// 交易流水号
        /// </summary>
        [Title("交易流水号")]
        public string BillingNo { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        [Title("操作员姓名")]
        public string OperatorName { get; set; }

        /// <summary>
        /// 使用者姓名
        /// </summary>
        [Title("使用者姓名")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [Title("交易类型")]
        [DataItem()]
        public string BillingType { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        [Title("支付方式")]
        [DataItem()]
        public string PayType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Title("金额")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        [Title("交易时间")]
        public DateTime CreateTime { get; set; }
    }
}
