using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.WebApiConfig
{
    /// <summary>
    /// 跳过的授权类型
    /// </summary>
    public enum SkipAuthType
    {
        /// <summary>
        /// 跳过认证与授权
        /// </summary>
        Both,
        /// <summary>
        /// 仅跳过授权
        /// </summary>
        JustAuthorization
    }

    /// <summary>
    /// 标记不需验证的方法或者服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
    public class DontNeedAuthAttribute : Attribute
    {
        private readonly SkipAuthType _skipAuthType;
        public SkipAuthType SkipOption => _skipAuthType;

        public DontNeedAuthAttribute(SkipAuthType skipAuthType = SkipAuthType.Both)
        {
            _skipAuthType = skipAuthType;
        }
    }
}
