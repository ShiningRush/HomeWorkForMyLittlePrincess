using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Core.Common.Const
{
    public class PlatformServiceConst
    {
        /// <summary>
        /// 访问授权过期提醒区间
        /// </summary>
        public const int ALERT_EXPIRE_MESSAGE_DAYS = 7;

        /// <summary>
        /// 公钥过期时间(分钟)
        /// </summary>
        public const int PUBLIC_KEY_EXPIRE_TIME = 5;

        /// <summary>
        /// TOKEN过期时间(分钟)
        /// </summary>
        public const int TOKEN_EXPIRE_TIME = 10;

        /// <summary>
        /// 临时文件目录
        /// </summary>
        public const string TEMP_FILE_NAME = "_tempFiles";

        /// <summary>
        /// 临时文件到期时间(默认2小时)
        /// </summary>
        public const long TEMP_FILE_EXPIRE_TIME = 1000 * 60 * 60 * 2;
    }
}
