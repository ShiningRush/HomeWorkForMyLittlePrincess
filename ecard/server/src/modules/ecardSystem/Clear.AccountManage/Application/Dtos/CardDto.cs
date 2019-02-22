using Abp.AutoMapper;
using Clear.AccountManage.Domain.Accounts;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Extensions;

namespace Clear.AccountManage.Application
{
    [AutoMapFrom(typeof(Card))]
    public class CardDto
    {
        /// <summary>
        /// 卡类型
        /// </summary>
        [DataItem]
        public string CardType { get;  set; }

        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// 卡号 
        /// </summary>
        public string CardNo { get;  set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CardStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get { return Status.GetDescription(); } }

        /// <summary>
        /// 是否验证支付密码
        /// </summary> 
        public bool IsPasswordAuth { get; set; }
    }
}
