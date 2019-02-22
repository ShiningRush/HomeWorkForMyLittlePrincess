using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using PlatformService.BridgeComponent.Domain.Entities;
using ServiceAnt.Handler;
using Clear.AccountManage.Domain.Accounts.DataItem;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 账户金额
    /// </summary> 
    [Table("ec_accountamount")]
    public class Capital: GeneratesDomainEvents
    {
        /// <summary>
        /// 账号代码
        /// </summary> 
        [MaxLength(64, ErrorMessage = "账号代码最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号代码不能为空")]
        [Column("AccountId", Order = 1)]
        [Key]
        public virtual string AccountId { get; set; }

        /// <summary>
        /// 资金类型 对应数据字典表 CapitalType 
        /// </summary> 
        [MaxLength(32, ErrorMessage = "资金类型最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "资金类型不能为空")]
        [Column("CapitalType",Order = 2)]
        [Key]
        public string CapitalType { get; set; }
        /// <summary>
        /// 金额
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "金额不能为空")]
        [Column("Balance")]
        public virtual decimal Balance { get; private set; }
        protected Capital() {  }

        public Capital(string accountId,string capitalType) {
            AccountId = accountId;
            CapitalType = capitalType;
        }

        /// <summary>
        /// 该更余额
        /// </summary>
        /// <param name="amount">需要变更的金额（大于0增加，小于0减少）</param>
        public void ChangeBalance(decimal amount, BillingType billingType, string payType = null, string desc = null)
        {
            if (amount < 0 && Balance < amount * -1)
            {
                throw new Exception("余额不足，扣费失败");
            }
            Balance += amount;
            DomainEvents.Add(new OnCapitalBalanceChanged(this, billingType, amount, payType, desc));
        }
    }
}
