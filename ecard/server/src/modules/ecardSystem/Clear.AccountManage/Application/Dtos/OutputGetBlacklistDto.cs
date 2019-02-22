using Clear.AccountManage.Domain.Accounts;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public class OutputGetBlacklistDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 卡状态
        /// </summary>
        public string CardStatus { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary> 
        public string IDCardNo { get; set; }

        /// <summary>
        /// 类型代码
        /// </summary>
        [DataItem("BlacklistType")]
        public string Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 有效期开始时间
        /// </summary> 
        public virtual DateTime BeginValidDate { get; set; }
        /// <summary>
        /// 有效期结束时间
        /// </summary> 
        public virtual DateTime EndValidDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
    }
}
