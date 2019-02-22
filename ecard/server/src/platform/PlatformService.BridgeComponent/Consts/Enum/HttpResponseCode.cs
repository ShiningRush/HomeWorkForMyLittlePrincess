using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Enum
{
    public enum HttpResponseCode
    {
        [Description("成功")]
        Success = 0,
        [Description("服务器发生了内部错误，请联系管理员")]
        Error = 1,
        [Description("无效的AppId")]
        InvalidAppId = 10000,
        [Description("错误的访问密钥")]
        ErrorAppSecret = 10001,
        [Description("Token已到期，请重新获取Token")]
        ExpiredToken = 10002,
        [Description("验证授权信息失败，请先获取授权信息。")]
        Unauthorized = 10003,
        [Description("该成员需要Get访问")]
        NeedGet = 10004,
        [Description("该成员需要Post访问")]
        NeedPost = 10005,
        [Description("无效的SyncKey或者SyncKey过期，请重新上报终端信息获取SyncKey")]
        ExpiredSyncKey = 10010,
    }
}
