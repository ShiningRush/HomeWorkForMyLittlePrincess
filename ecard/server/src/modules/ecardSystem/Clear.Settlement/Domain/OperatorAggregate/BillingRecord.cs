using Abp.Domain.Entities;
using Abp.Events.Bus;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Domain.OperatorAggregate
{
    /// <summary>
    /// 交易&充值记录
    /// </summary> 
    [Table("ec_billingrecord")]
    public class BillingRecord : Entity<long>, IMayHaveCreator, IMustHaveOrganize
    {
        public BillingRecord() {
            CreateTime = DateTime.Now;
        }

        public BillingRecord(
            string billingNo ,string accountId,string capitalType,decimal amount,string payType,string desc, string cardNo, string billType,decimal balance) :this()
        {
            BillingNo = billingNo;
            AccountId = accountId;
            CapitalType = capitalType;
            AppId = capitalType;
            Amount = amount;
            PayType = payType;
            Desc = desc;
            CardNo = cardNo;
            BillingType = billType;
            Balance = balance;
        }

        ///// <summary>
        ///// Id
        ///// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Id不能为空")]
        //[Key]
        //[Column("Id", Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public override long Id { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        [MaxLength(20, ErrorMessage = "交易流水号最大长度20")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "交易流水号不能为空")]
        public string BillingNo { get;private set; }

        /// <summary>
        /// 账号代码
        /// </summary> 
        [MaxLength(64, ErrorMessage = "账号代码最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号代码不能为空")]
        [Column("AccountId")]
        public virtual string AccountId { get; set; }

        /// <summary>
        /// 资金类型 对应数据字典表 CapitalType 
        /// </summary> 
        [MaxLength(32, ErrorMessage = "资金类型最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "资金类型不能为空")]
        [Column("CapitalType")]
        public string CapitalType { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary> 
        [MaxLength(64, ErrorMessage = "应用ID最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "应用ID不能为空")]
        [Column("AppId")]
        public virtual string AppId { get; set; }
        /// <summary>
        /// 流水类型 对应字典代码 BillingType
        /// </summary> 
        [MaxLength(32, ErrorMessage = "流水类型最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "流水类型不能为空")]
        [Column("BillingType")]
        public virtual string BillingType { get; set; }
        /// <summary>
        /// 支付方式 对应字典代码 PayType
        /// </summary> 
        [MaxLength(32, ErrorMessage = "支付方式最大长度32")]
        [Column("PayType")]
        public virtual string PayType { get; set; }
        /// <summary>
        /// 卡号
        /// </summary> 
        [MaxLength(32, ErrorMessage = "卡号最大长度32")]
        [Column("CardNo")]
        public virtual string CardNo { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "机构主键不能为空")]
        [Column("OrganizeId")]
        public Guid OrganizeId { get; set; }
        /// <summary>
        /// 金额
        /// </summary> 
        [Column("Amount")]
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// 余额 变更后的余额
        /// </summary> 
        [Column("Balance")]
        public virtual decimal Balance { get; set; }

        
        /// <summary>
        /// 描述
        /// </summary> 
        [MaxLength(256, ErrorMessage = "描述最大长度256")]
        [Column("Desc")]
        public virtual string Desc { get; set; }
        /// <summary>
        /// 访问系统流水号
        /// </summary> 
        [MaxLength(32, ErrorMessage = "访问系统流水号最大长度32")]
        [Column("AccessBillingNo")]
        public virtual string AccessBillingNo { get; set; }
        /// <summary>
        /// 结算记录Id
        /// </summary> 
        [Column("SettlementRecordId")]
        public virtual Guid? SettlementRecordId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        [Column("Creator")]
        public Guid? Creator { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "交易时间不能为空")]
        //[Column("CreateTime", Order = 2)]
        //[Key()]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary> 
        [MaxLength(32, ErrorMessage = "终端类型最大长度32")]
        public virtual string TerminalType{ get; set; }

        /// <summary>
        /// 终端编号
        /// </summary> 
        [MaxLength(32, ErrorMessage = "支付方式最大长度32")]
        public virtual string TerminalNo { get; set; }

        /// <summary>
        /// 应用场景
        /// </summary> 
        [MaxLength(32, ErrorMessage = "应用场景最大长度32")]
        public virtual string ApplicationScene { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary> 
        [MaxLength(32, ErrorMessage = "业务类型最大长度32")]
        public virtual string BusinessType { get; set; }

        /// <summary>
        /// 所属帐户
        /// </summary>
        [ForeignKey("AccountId")]
        public virtual Account OwedAccount { get; set; }

        /// <summary>
        /// 所属操作人
        /// </summary>
        [ForeignKey("Creator")]
        public virtual Operator OwedOperator { get; set; }
    }
}
