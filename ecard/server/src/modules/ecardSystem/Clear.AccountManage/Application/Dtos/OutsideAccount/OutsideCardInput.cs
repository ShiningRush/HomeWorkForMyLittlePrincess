using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public class OutsideCardInput
    {
        /// <summary>
        /// 卡类型
        /// </summary>
        [DataItem]
        public virtual string CardType { get; set; }


        /// <summary>
        /// 卡号 
        /// </summary>
        [MaxLength(32, ErrorMessage = "卡号最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡号不能为空")]
        public virtual string CardNo { get; set; }
    }
}
