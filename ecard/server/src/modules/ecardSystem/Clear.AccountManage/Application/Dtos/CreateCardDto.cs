using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public class CreateCardDto
    {
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 卡号 
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 是否验证支付密码
        /// </summary> 
        public bool IsPasswordAuth { get; set; }
    }
}
