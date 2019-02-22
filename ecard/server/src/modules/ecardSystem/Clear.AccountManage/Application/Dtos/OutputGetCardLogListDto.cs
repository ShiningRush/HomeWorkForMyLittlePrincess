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
    [AutoMapFrom(typeof(CardLog))]
    public class OutputGetCardLogListDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 卡号信息
        /// </summary>
        public string CardNo { get; set; }

        public string Name { get; set; }
        public string IDCardType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary> 
        public string IDCardNo { get; set; }
        /// <summary>
        /// 操作类型（卡务类型）
        /// </summary>
        /// 
        [DataItem("CardOperationType")]
        public string OperationType { get; set; }
        /// <summary>
        /// 操作类型（卡务类型）
        /// </summary>
        public string OperationTypeName { get; set; }
        /// <summary>
        /// Balance 余额（Capital中字段）
        /// </summary>
        public decimal? Balance { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注：原因
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public virtual Guid? Creator { get; set; }

        public string CreatorName { get; set; }
    }
}
