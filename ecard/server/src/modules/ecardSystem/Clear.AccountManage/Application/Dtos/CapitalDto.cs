using Abp.AutoMapper;
using Clear.AccountManage.Domain.Accounts;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    [AutoMapFrom(typeof(Capital))]
    public class CapitalDto
    {
        /// <summary>
        /// 资金类型 对应数据字典表 CapitalType 
        /// </summary> 
        [DataItem]
        public string CapitalType { get; set; }
        /// <summary>
        /// 资金类型名称
        /// </summary>
        public string CapitalTypeName { get; set; }

        /// <summary>
        /// 金额
        /// </summary> 
        public virtual decimal Balance { get;  set; }
    }
}
