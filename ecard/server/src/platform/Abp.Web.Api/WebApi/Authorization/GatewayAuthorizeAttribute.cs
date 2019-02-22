using Abp.Dependency;
using Abp.WebApi.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Threading;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.Runtime.Caching;
using Castle.Core.Logging;
using PlatformService.BridgeComponent.Data;
using PlatformService.BridgeComponent.Enum;
using PlatformService.BridgeComponent.Authorization;
using PlatformService.BridgeComponent.Service;
using PlatformService.BridgeComponent.Service.Session;

namespace Yiban.CoreService.Web.Api.WebApi.Authorization
{
    // *King
    public class GatewayAuthorizeAttribute : AuthorizeAttribute , ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IAbpWebApiConfiguration _configuration;
        private readonly IServiceContext _serviceContext;
        private readonly ISessionManager _sessionManager;

        public IPasswordAuthorization PassworkAuthorization { get; set; }
        public IClientAuthorization ClientAuthorization { get; set; }

        public GatewayAuthorizeAttribute(
            IAbpWebApiConfiguration configuration,
            IServiceContext serviceContext,
            ISessionManager sessionManager)
        {
            _configuration = configuration;
            _serviceContext = serviceContext;
            _sessionManager = sessionManager;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //return true;
            var needAuthentication = (bool)actionContext.ActionDescriptor.Properties.GetOrAdd("__IsGatewayNeedAuthenticate", true);
            var needAuthorization = (bool)actionContext.ActionDescriptor.Properties.GetOrAdd("__IsGatewayNeedAuthorize", true);
            // 不需要认证, 直接通过
            if (!needAuthentication)
            {
                return true;
            }

            if (base.IsAuthorized(actionContext) && needAuthorization)
            {
                // 不需要授权的接口, 通过认证后即可
                if (!needAuthorization)
                    return true;

                //Get action name from route
                var serviceNameWithAction = (actionContext.ControllerContext.RouteData.Values["serviceNameWithAction"] as string);
                //var actionName = DynamicApiServiceNameHelper.GetActionNameInServiceNameWithAction(serviceNameWithAction);

                if (_serviceContext.GetValue("grant_type").Equals("password") &&
                PassworkAuthorization != null &&
                PassworkAuthorization.CheckMethodAuthorization(_serviceContext.ClientID, serviceNameWithAction))
                {
                    // password 模式为有状态模式, 需要检测服务端会话是否过期
                    if (_sessionManager.GetSessionInfo() == null)
                    {
                        Logger.DebugFormat("password授权验证通过, 但会话已经过期，访问路径{0}", serviceNameWithAction);
                        return false;
                    }

                    Logger.DebugFormat("password授权码已验证通过且具备该访问权限，访问路径{0}", serviceNameWithAction);
                    return true;
                }

                if (_serviceContext.GetValue("grant_type").Equals("client") &&
                    ClientAuthorization != null &&
                    ClientAuthorization.CheckMethodAuthorization(_serviceContext.ClientID, serviceNameWithAction))
                {
                    Logger.DebugFormat("client授权码已验证通过且具备该访问权限，访问路径{0}", serviceNameWithAction);
                    return true;
                }

                Logger.DebugFormat("授权码已验证通过，但不具备访问权限，访问路径{0}", serviceNameWithAction);
                return false;
            }

            return false;
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            actionContext.Response.Content = new ObjectContent<RespDataBase>(
                new RespDataBase(HttpResponseCode.Unauthorized),
                _configuration.HttpConfiguration.Formatters.JsonFormatter
                );
        }
    }
}
