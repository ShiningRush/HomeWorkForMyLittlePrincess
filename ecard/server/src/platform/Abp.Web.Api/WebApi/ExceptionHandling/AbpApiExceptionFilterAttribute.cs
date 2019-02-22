using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Extensions;
using Abp.Logging;
using Abp.Runtime.Session;
using Abp.Web.Models;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers;
using Castle.Core.Logging;
using Abp.Runtime.Validation;
using System.Text;
using PlatformService.BridgeComponent.CustomException;

namespace Abp.WebApi.ExceptionHandling
{
    /// <summary>
    /// Used to handle exceptions on web api controllers.
    /// </summary>
    public class AbpApiExceptionFilterAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        /// <summary>
        /// Reference to the <see cref="ILogger"/>.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to the <see cref="IEventBus"/>.
        /// </summary>
        public IEventBus EventBus { get; set; }

        public IAbpSession AbpSession { get; set; }

        private readonly IAbpWebApiConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbpApiExceptionFilterAttribute"/> class.
        /// </summary>
        public AbpApiExceptionFilterAttribute(IAbpWebApiConfiguration configuration)
        {
            _configuration = configuration;
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var wrapResultAttribute = HttpActionDescriptorHelper
                .GetWrapResultAttributeOrNull(context.ActionContext.ActionDescriptor) ??
                _configuration.DefaultWrapResultAttribute;

            if (wrapResultAttribute.LogError)
            {
                var customHttpEx = context.Exception as CustomHttpException;
                // *King
                if (customHttpEx != null)
                {
                    Logger.WarnFormat("WebApi中发生了某些可预知的错误: {0}", customHttpEx.GetRespData().Message);
                }
                else
                {
                    LogHelper.LogException(Logger, context.Exception);
                }
            }

            if (!wrapResultAttribute.WrapOnError)
            {
                return;
            }

            if (IsIgnoredUrl(context.Request.RequestUri))
            {
                return;
            }

            context.Response = context.Request.CreateResponse(
                GetStatusCode(context),
                ErrorChangeToHttpHandle(context.Exception).GetRespData()
            );

            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));
        }

        // *King
        private CustomHttpException ErrorChangeToHttpHandle(Exception ex)
        {
            // defualt error
            CustomHttpException httpException = CustomHttpException.GetHttpErrorFromEx(ex);

            if (ex is AggregateException && ex.InnerException != null)
            {
                var aggException = ex as AggregateException;
                if (aggException.InnerException is CustomHttpException ||
                    aggException.InnerException is AbpValidationException)
                {
                    ex = aggException.InnerException;
                }
            }

            if (ex is AbpValidationException)
            {
                var abpValidErr = ex as AbpValidationException;
                var validErrMsg = new StringBuilder();
                foreach (var error in abpValidErr.ValidationErrors)
                {
                    if (error.ErrorMessage.IsNullOrEmpty())
                        error.ErrorMessage = "参数格式不对，请重新确认输入参数。";
                    validErrMsg.AppendLine(string.Format("输入参数：{0} 发生验证错误：{1}", string.Join("，", error.MemberNames), error.ErrorMessage));
                }

                httpException = new CustomHttpException(validErrMsg.ToString());
            }
            else if(ex is EntityNotFoundException)
            {
                var entityNotFoundException = ex as EntityNotFoundException;
                httpException = new CustomHttpException($"{entityNotFoundException.EntityType.Name}不存在ID：{entityNotFoundException.Id}");
            }

            return httpException;
        }

        private HttpStatusCode GetStatusCode(HttpActionExecutedContext context)
        {
            if (context.Exception is Abp.Authorization.AbpAuthorizationException)
            {
                return AbpSession.UserId.HasValue
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            // *King
            //if (context.Exception is EntityNotFoundException)
            //{
            //    return HttpStatusCode.NotFound;
            //}

            // *King
            return HttpStatusCode.OK;
        }

        private bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || uri.AbsolutePath.IsNullOrEmpty())
            {
                return false;
            }

            return _configuration.ResultWrappingIgnoreUrls.Any(url => uri.AbsolutePath.StartsWith(url));
        }
    }
}