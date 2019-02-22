using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.AppServices.Dtos
{
    public class GetNotSettledBillingGraphDataOutput
    {
        /// <summary>
        /// 费用清单项目Key
        /// </summary>
        public string ItemKey { get; set; }

        /// <summary>
        /// 费用清单项目值
        /// </summary>
        public string ItemValue { get; set; }
    }
}
