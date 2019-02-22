using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Domain.OperatorAggregate
{
    /// <summary>
    /// 结算信息
    /// </summary> 
    [Table("ec_settlement")]
    public class SettlementRecord : Entity<Guid>, IMustHaveCreator, IMustHaveOrganize
    {
        public SettlementRecord() { }

        public SettlementRecord(Operator ownedOperator, DateTime settleEndTime)
        {
            this.Creator = ownedOperator.Id;
            this.SettleEndTime = settleEndTime;
        }

        /// <summary>
        /// 所属机构Id
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "机构Id不能为空")]
        [Column("OrganizeId")]
        public virtual Guid OrganizeId { get; set; }
        /// <summary>
        /// 金额内容Json
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "金额内容Json不能为空")]
        [Column("AmountContent")]
        [MaxLength(2048, ErrorMessage = "创建人最大长度2048")]
        public virtual string AmountContent { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "创建人不能为空")]
        [Column("Creator")]
        public virtual Guid Creator { get; set; }
        /// <summary>
        /// 结算结束时间
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "结算结束时间不能为空")]
        [Column("SettleEndTime")]
        public virtual DateTime SettleEndTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "创建时间不能为空")]
        [Column("CreateTime")]
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 所属操作人
        /// </summary>
        [ForeignKey("Creator")]
        public virtual Operator OwedOperator { get; set; }
    }
}
