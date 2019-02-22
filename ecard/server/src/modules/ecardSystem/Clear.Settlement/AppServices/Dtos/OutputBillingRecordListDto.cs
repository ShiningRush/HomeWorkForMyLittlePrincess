using Clear.AccountManage.Application;
using Clear.Component.Interfaces.Componets.DataFormat;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public class OutputBillingRecordListDto
    {
        /// <summary>
        /// 人信息
        /// </summary>
        [Title("账户ID")]
        public string AccountId { get; set; }
        /// <summary>
        /// 人信息
        /// </summary>
        [Title("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 资金
        /// </summary>
        [Title("资金")]
        public string Amount { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        [Title("余额")]
        public decimal? Balance { get; set; }
        /// <summary>
        /// 创建时间（交易时间）
        /// </summary>
        [Title("操作时间")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 应用场景
        /// </summary>
        [Title("应用场景")]
        public  string ApplicationScene { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [Title("操作员")]
        public string UserName { get; set; }
        /// <summary>
        /// 资金类型
        /// </summary>
        [Title("资金类型")]
        public string CapitalType { get; set; }
        /// <summary>
        /// 资金类型
        /// </summary>
        [Title("流水类型")]
        public string BillingType { get; set; }
    }
}
