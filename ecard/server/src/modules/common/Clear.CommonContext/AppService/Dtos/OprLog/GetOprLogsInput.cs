using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.CommonContext.AppService.Dtos.OprLog
{
    public class GetOprLogsInput : PagerParameter
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }
        public string ClientIpAddress { get; set; }
        
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
