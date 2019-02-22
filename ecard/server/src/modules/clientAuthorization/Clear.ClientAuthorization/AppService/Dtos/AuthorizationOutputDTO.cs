using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.AppService
{
    public class AuthorizationOutputDTO
    {
        /// <summary>
        /// 记录Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 接入应用Id
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 应用名
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop { get; set; }
    }
}
