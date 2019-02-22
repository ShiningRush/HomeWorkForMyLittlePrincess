using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public class OutsideFeeInput: OutsideCardInput
    {

        /// <summary>
        /// 资金类型
        /// </summary>
        [DataItem]
        public virtual string CapitalType { get; set; }

        /// <summary>
        /// 退费金额
        /// </summary>
        public virtual decimal Amount { get; set; }
    }
}
