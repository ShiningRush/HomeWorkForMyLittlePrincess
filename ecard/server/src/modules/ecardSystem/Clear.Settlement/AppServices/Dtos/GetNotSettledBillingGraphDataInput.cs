﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.AppServices.Dtos
{
    public class GetNotSettledBillingGraphDataInput
    {
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettleTime { get; set; }
    }
}
