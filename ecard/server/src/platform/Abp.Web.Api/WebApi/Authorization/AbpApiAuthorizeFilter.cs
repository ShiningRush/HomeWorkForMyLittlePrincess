using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Localization;
using Abp.Logging;
using Abp.Web;
using Abp.Web.Models;
using Abp.WebApi.Configuration;
using Abp.WebApi.Validation;
using PlatformService.BridgeComponent.Data;

namespace Abp.WebApi.Authorization
{
    public class AbpApiAuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        public bool AllowMultiple => false;

        private readonly IAuthorizationHelper _authorizationHelper;
        private readonly IAbpWebApiConfiguration _configuration;
        private readonly ILocalizationManager _localizationManager;
        private readonly IEventBus _eventBus;

        public AbpApiAuthorizeFilter(
            IAuthorizationHelper authorizationHelper, 
            IAbpWebApiConfiguration configuration,
            ILocalizationManager localizationManager,
            IEventBus eventBus)
        {
            _authorizationHelper = authorizationHelper;
            _configuration = configuration;
            _localizationManager = localizationManager;
            _eventBus = eventBus;
        }

        public virtual async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (actionContext.ActionDescriptor.IsDynamicAbpAction())
            {
                return await continuation();
            }

            try
            {
                await _authorizationHelper.AuthorizeAsync(methodInfo);
                return await continuation();
            }
            catch (AbpAuthorizationException ex)
            {
                LogHelper.Logger.Warn(ex.ToString(), ex);
                _eventBus.Trigger(this, new AbpHandledExceptionData(ex));
                return CreateUnAuthorizedResponse(actionContext);
            }
        }

        protected virtual HttpResponseMessage CreateUnAuthorizedResponse(HttpActionContext actionContext)
        {
            HttpStatusCode statusCode = HttpStatusCode.Unauthorized;

            var response = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<RespData>(
                    new RespData(),
                    _configuration.HttpConfiguration.Formatters.JsonFormatter
                )
            };

            return response;
        }
    }
}