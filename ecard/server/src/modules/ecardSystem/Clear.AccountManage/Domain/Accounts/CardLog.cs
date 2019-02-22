using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 卡务日志
    /// </summary> 
    [Table("ec_cardlog")]
    public partial class CardLog : Entity<Guid>, IMustHaveOrganize, IMayHaveCreator
    {
        /// <summary>
        /// 主键
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键不能为空")]
        [Column("Id")]
        public override Guid Id { get; set; }
        /// <summary>
        /// 账号代码
        /// </summary> 
        [MaxLength(36, ErrorMessage = "账号代码最大长度36")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号代码不能为空")]
        [Column("AccountId")]
        public virtual string AccountId { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary> 
        [MaxLength(64, ErrorMessage = "应用ID最大长度64")]
        [Column("AppId")]
        public virtual string AppId { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary> 
        [MaxLength(32, ErrorMessage = "操作类型最大长度32")]
        [Column("OperationType")]
        public virtual string OperationType { get; set; }
        /// <summary>
        /// 卡号
        /// </summary> 
        [MaxLength(32, ErrorMessage = "卡号最大长度32")]
        [Column("CardNo")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary> 
        [Column("OrganizeId")]
        public Guid OrganizeId { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [MaxLength(256, ErrorMessage = "备注最大长度256")]
        [Column("Remark")]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        [Column("Creator")]
        public virtual Guid? Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        [Column("CreateTime")]
        public virtual DateTime CreateTime { get; set; }
    }

}
