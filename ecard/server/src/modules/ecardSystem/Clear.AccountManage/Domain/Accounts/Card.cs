using Abp.Domain.Entities;
using Clear.AccountManage.Domain.Accounts.DataItem;
using JetBrains.Annotations;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 卡信息
    /// </summary>
    /// <remarks></remarks>
    [Table("ec_card")]
    public class Card: EntityWithGeneratesDomainEvents<Guid>
    {
        protected Card() { }

        public Card([NotNull] string accountId, [NotNull]  string cardType, [NotNull] string cardNo, bool isPasswordAuth = false)
        {
            AccountId = accountId;
            CardType = cardType;
            CardNo = cardNo;
            Status = CardStatus.Normal;
            IsPasswordAuth = isPasswordAuth;
        }

        /// <summary>
        /// 所属的账户ID
        /// </summary>
        [Index("Idx_Unique_AccountId_CardType", Order = 1, IsUnique = true)]
        [MaxLength(64, ErrorMessage = "所属的账户ID最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "所属的账户ID不能为空")]
        public string AccountId { get;  set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        [Index("Idx_Unique_AccountId_CardType", Order =2, IsUnique = true)]
        [Index("Idx_Unique_CardType_CardNo", Order = 1, IsUnique = true)]
        [MaxLength(32, ErrorMessage = "卡类型最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡类型不能为空")]
        public string CardType { get;  set; }


        /// <summary>
        /// 卡号 
        /// </summary>
        [MaxLength(32, ErrorMessage = "卡号最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡号不能为空")]
        [Column("CardNo")]
        [Index("Idx_Unique_CardType_CardNo", Order = 2, IsUnique = true)]
        public string CardNo { get;  set; }//TODO 卡号是否可以直接修改？

        /// <summary>
        /// 状态
        /// </summary>
        public CardStatus Status { get;  set; }

        /// <summary>
        /// 是否验证支付密码
        /// </summary> 
        [Column("IsPasswordAuth")]
        public bool IsPasswordAuth { get; set; }

        /// <summary>
        /// 挂失卡
        /// </summary>
        public void Loss(string reason)
        {
            if (Status == CardStatus.Normal)
            {
                Status = CardStatus.Loss;
                DomainEvents.Add(new OnCardChanged(this,CardOperationType.LossCard, reason));
            }
        }

        /// <summary>
        /// 卡状态设置为正常状态
        /// </summary>
        public void ReleaseLoss(string reason)
        {
            if (Status == CardStatus.Loss)
            {
                Status = CardStatus.Normal;
                DomainEvents.Add(new OnCardChanged(this, CardOperationType.ReleaseLoss, reason));
            }
        }
    }
}
