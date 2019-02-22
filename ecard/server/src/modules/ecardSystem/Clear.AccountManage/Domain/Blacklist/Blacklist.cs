using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Blacklist
{
    /// <summary>
    /// 黑/白名单
    /// </summary> 
    [Table("ec_blacklist")]
    public partial class Blacklist : Entity<Guid>, IMayHaveCreator,IHasModifier
    {
        /// <summary>
        /// 卡号Id
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡号Id不能为空")]
        [Column("CardId")]
        public virtual Guid CardId { get; set; }
        [ForeignKey("CardId")]
        public virtual Accounts.Card Card { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary> 
        [MaxLength(64, ErrorMessage = "应用ID最大长度64")]
        [Column("AppId")]
        public virtual string AppId { get; set; }
        /// <summary>
        /// 类型
        /// </summary> 
        [MaxLength(64, ErrorMessage = "类型最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "类型不能为空")]
        [Column("Type")]
        public virtual string Type { get; set; }
        /// <summary>
        /// 原因
        /// </summary> 
        [MaxLength(256, ErrorMessage = "原因最大长度256")]
        [Column("Remark")]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 有效期开始时间
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "有效期开始时间不能为空")]
        [Column("BeginValidDate")]
        public virtual DateTime BeginValidDate { get; set; }
        /// <summary>
        /// 有效期结束时间
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "有效期结束时间不能为空")]
        [Column("EndValidDate")]
        public virtual DateTime EndValidDate { get; set; }
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
        /// <summary>
        /// 最后修改人
        /// </summary> 
        [Column("LastModifier")]
        public virtual Guid? LastModifier { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary> 
        [Column("LastModifyTime")]
        public virtual DateTime? LastModifyTime { get; set; }
    }

}
